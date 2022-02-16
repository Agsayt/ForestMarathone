using System;
using System.Collections.Generic;
using System.Linq;
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

            
            //Thread thread = new Thread(new ThreadStart(startRotation));
            //thread.Start();
        }

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

            using (var context = new ForestMarathoneEntities())
            {
                Users user = (Users)context.Users.Where(x => x.Login == _login.Text && x.Password == _password.Password).FirstOrDefault();

                bool isVerified = user != null ? true: false;

                if (isVerified)
                {
                    switch (user.RoleId)
                    {
                        case 0: Administrator administrator = context.Administrator.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(administrator); break;
                        case 1: Participant participant = context.Participant.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(participant); break;
                        case 2: Sponsors sponsor = context.Sponsors.Where(x => x.Id == user.Id).FirstOrDefault(); ToRoleMainPage(sponsor); break;
                    }
                }
                else
                {

                }
            }
        }

        private void ToRoleMainPage(object sender)
        {
            var roleWindow = new Pages.Administrator.AdministratorMainMenu();
            this.Hide();
            roleWindow.ShowDialog();
            this.Show();
            
        }
    }
}
