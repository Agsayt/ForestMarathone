using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
    }
}
