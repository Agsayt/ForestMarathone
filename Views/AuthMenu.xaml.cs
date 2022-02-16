using ForestMarathone.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Threading;

namespace ForestMarathone.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthMenu : Window
    {
        public AuthMenu()
        {
            InitializeComponent();
            _flipView.HideControlButtons();
            _flipView.BannerText = null;
            _flipView.IsEnabled = false;

            DataContext = new AuthViewModel();

            using (var context = new ForestMarathoneEntities())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [LoginHistory]");
            }

            this.Closing += onClose;

            int change = 1;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (o, a) =>
            {
                int newIndex = _flipView.SelectedIndex + change;
                if (newIndex >= _flipView.Items.Count || newIndex < 0)
                {
                    change *= -1;
                }

                _flipView.SelectedIndex += change;
            };

            timer.Start();
        }

        private void onClose(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        ForestMarathone.Administrator administrator;
        ForestMarathone.Participant participant;
        ForestMarathone.Sponsors sponsor;
        float waitTime = 0f;
        System.Timers.Timer failedAttemptTimer;
        bool isSuspended = false;

        

        private void _authButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isSuspended)
            {
                using (var context = new ForestMarathoneEntities())
                {
                    Users user = (Users)context.Users.Where(x => x.Login == _login.Text && x.Password == _password.Password).FirstOrDefault();

                    bool isVerified = user != null ? true : false;

                    if (isVerified && user.UserStatusId == UStatus.Activated)
                    
                    {
                        switch (user.UserStatusId)
                        {
                            case UStatus.Activated:

                                context.LoginHistory.Add(new LoginHistory
                                {
                                    Login = _login.Text,
                                    IPAddress = GetIPAddress().ToString(),
                                    ConnectionTime = DateTime.Now.TimeOfDay,
                                    ValidAuth = true
                                });
                                context.SaveChanges();

                                switch (user.RoleId)
                                {
                                    case Role.Administrator: administrator = context.Administrator.Where(x => x.UserId == user.UserId).FirstOrDefault(); ToRoleMainPage(administrator, user); break;
                                    case Role.Participant: participant = context.Participant.Where(x => x.UserId == user.UserId).FirstOrDefault(); ToRoleMainPage(participant, user); break;
                                    case Role.Sponsor: sponsor = context.Sponsors.Include("Users").Where(x => x.UserId == user.UserId).FirstOrDefault(); ToRoleMainPage(sponsor, user); break;
                                }

                                break;
                            case UStatus.Suspended:
                                MessageBoxResult mb = MessageBox.Show("Ваша учётная запись заморожена! Обратитесь к администратору!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            case UStatus.Banned:
                                MessageBoxResult mbs = MessageBox.Show("Ваша учётная запись заблокирована! Обратитесь к администратору!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);

                                break;
                        }


                    }
                    else
                    {
                        context.LoginHistory.Add(new LoginHistory
                        {
                            Login = _login.Text,
                            IPAddress = GetIPAddress().ToString(),
                            ConnectionTime = DateTime.Now.TimeOfDay,
                            ValidAuth = false
                        });
                        context.SaveChanges();

                        if(context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 2)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 2)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль! Вы заблокированы на 15 секунд!", "Ошибка!", MessageBoxButton.OK,MessageBoxImage.Error);
                            waitTime = 15000f;
                            StartSuspend();
                        }
                        else if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 3)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль! Вы заблокированы на 20 секунд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            waitTime = 20000f;
                            StartSuspend();
                        }
                        else if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 4)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль! Вы заблокированы, программа будет выключена!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                            Application.Current.Shutdown();
                        }                        
                    }

                }
            }

           
        }

        private void StartSuspend()
        {
            isSuspended = true;
            _authButton.Content = "Заблокировано!";
            failedAttemptTimer = new System.Timers.Timer(waitTime);
            failedAttemptTimer.Elapsed += AllowAccess;
            failedAttemptTimer.Start();
            _authButton.IsEnabled = false;
        }

        private void AllowAccess(object sender, EventArgs e)
        {
            _authButton.Dispatcher.BeginInvoke(new Action(() =>
            {
                _authButton.IsEnabled = true;
                _authButton.Content = "Войти";

            }));
            isSuspended = false;
            failedAttemptTimer.Close();
        }

        private void ToRoleMainPage(object sender, Users user)
        {
            if (sender is ForestMarathone.Administrator)
            {                
                var roleWindow = new Pages.Administrator.AdministratorMainMenu(administrator);
                this.Hide();
                roleWindow.ShowDialog();
            }
            else if (sender is ForestMarathone.Participant || user.RoleId == Role.Participant)
            {
                Participant.ParticipantMenu roleWindow = null;
                if (participant == null)
                    roleWindow = new Participant.ParticipantMenu(user);
                else
                    roleWindow = new Participant.ParticipantMenu(participant);
                this.Hide();
                roleWindow.ShowDialog();
            }
            else if (sender is ForestMarathone.Sponsors || user.RoleId == Role.Sponsor)
            {
                Sponsor.SponsorMenu roleWindow = null;
                if (sponsor == null)
                    roleWindow = new Sponsor.SponsorMenu(user);
                else
                    roleWindow = new Sponsor.SponsorMenu(sponsor);
                this.Hide();
                roleWindow.ShowDialog();
            }
            else
                return;            
        }


        public static IPAddress GetIPAddress()
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses("");

            foreach (IPAddress hostAddress in hostAddresses)
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(hostAddress) &&  
                    !hostAddress.ToString().StartsWith("169.254.")) 
                    return hostAddress;
            }
            return null; 
        }
    }
}
