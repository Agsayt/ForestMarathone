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
    /// Логика взаимодействия для LoggerMenu.xaml
    /// </summary>
    public partial class LoggerMenu : Window
    {
        public LoggerMenu()
        {
            InitializeComponent();


            using (var context = new ForestMarathoneEntities())
            {
                var source = context.LoginHistory.Select(x => x).ToList();
                _history.ItemsSource = source;
            }
        }
    }
}
