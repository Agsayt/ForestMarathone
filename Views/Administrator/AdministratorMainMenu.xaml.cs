using ForestMarathone.Views.Administrator;
using System.Windows;

namespace ForestMarathone.Pages.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdministratorMainMenu.xaml
    /// </summary>
    public partial class AdministratorMainMenu : Window
    {
        private ForestMarathone.Administrator administrator {get; set;}

        public AdministratorMainMenu(ForestMarathone.Administrator sender)
        {
            InitializeComponent();
            administrator = sender;
        }

        private void _logger_Click(object sender, RoutedEventArgs e)
        {
            var logPage = new LoggerMenu();
            this.Hide();
            logPage.ShowDialog();
            this.Show();
        }

        private void _accounts_Click(object sender, RoutedEventArgs e)
        {
            var accMan = new AdministratorAccountManagement();
            this.Hide();
            accMan.ShowDialog();
            this.Show();

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.ShowDialog();
        }
    }
}
