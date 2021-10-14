using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernWpf;

namespace ForestMarathone
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

            using (var context = new ForestMarathoneEntities())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [LoginHistory]");
            }

            this.Closing += onClose;

            //Thread thread = new Thread(new ThreadStart(startRotation));
            //thread.Start();
        }

        private void onClose(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        Administrator administrator;
        Participant participant;
        Sponsors sponsor;
        float waitTime = 0f;
        System.Timers.Timer failedAttemptTimer;
        bool isSuspended = false;

        public void startRotation()
        {
            System.Timers.Timer timerFlipView = new System.Timers.Timer(2500);
            timerFlipView.Elapsed += RotateFlipView;
            timerFlipView.Start();
        }

        public void RotateFlipView(object sender, EventArgs e)
        {

            int? nextIndex = ++_flipView.SelectedIndex;
            if (nextIndex != null)
            {
                _flipView.SelectedIndex++;
            }
        }

        private void _authButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isSuspended)
            {
                using (var context = new ForestMarathoneEntities())
                {
                    Users user = (Users)context.Users.Where(x => x.Login == _login.Text && x.Password == _password.Password).FirstOrDefault();

                    bool isVerified = user != null ? true : false;

                    if (isVerified && user.UserStatusId == 1)
                    {
                        switch (user.UserStatusId)
                        {
                            case 1:

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
                                    case 0: administrator = context.Administrator.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(administrator); break;
                                    case 1: participant = context.Participant.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(participant); break;
                                    case 2: sponsor = context.Sponsors.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(sponsor); break;
                                }

                                break;
                            case 2:
                                MessageBoxResult mb = MessageBox.Show("Ваша учётная запись заморожена! Обратитесь к администратору!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            case 3:
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

                        if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 1)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль! Вы заблокированы на 15 секунд!", "Ошибка!", MessageBoxButton.OK,MessageBoxImage.Error);
                            waitTime = 15000f;
                            StartSuspend();
                        }
                        else if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 2)
                        {
                            MessageBoxResult mb = MessageBox.Show("Введён неверный логин или пароль! Вы заблокированы на 20 секунд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            waitTime = 20000f;
                            StartSuspend();
                        }
                        else if (context.LoginHistory.Where(x => x.Login == _login.Text).Count() == 3)
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
            failedAttemptTimer = new System.Timers.Timer(waitTime);
            failedAttemptTimer.Elapsed += AllowAccess;
            failedAttemptTimer.Start();
            _authButton.IsEnabled = false;
        }

        private void AllowAccess(object sender, EventArgs e)
        {
            _authButton.IsEnabled = true;
            isSuspended = false;
            failedAttemptTimer.Close();
        }

        private void ToRoleMainPage(object sender)
        {
            var roleWindow = new Pages.Administrator.AdministratorMainMenu(administrator);
            this.Hide();
            roleWindow.ShowDialog();
            this.Show();
            
        }


        public static IPAddress GetIPAddress()
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses("");

            foreach (IPAddress hostAddress in hostAddresses)
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(hostAddress) &&  // ignore loopback addresses
                    !hostAddress.ToString().StartsWith("169.254."))  // ignore link-local addresses
                    return hostAddress;
            }
            return null; // or IPAddress.None if you prefer
        }
    }
}
