using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ForestMarathone.Pages.Administrator
{
    /// <summary>
    /// Логика взаимодействия для LoggerMenu.xaml
    /// </summary>
    public partial class LoggerMenu : Window
    {
        List<LoginHistory> sourceList { get;set; }
        private ICollectionView _dataGridCollection;
        private string _filterString;

        public LoggerMenu()
        {
            InitializeComponent();

            

            using (var context = new ForestMarathoneEntities())
            {
                var source = context.LoginHistory.Select(x => x).ToList();
                sourceList = new List<LoginHistory>();
                sourceList.AddRange(source);
                _history.ItemsSource = source;
                _comboBox.SelectedIndex = 0;
                _comboBox.SelectionChanged += _history_SelectionChanged;

            }


        }

        private void _history_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            using (var context = new ForestMarathoneEntities())
            {
                sourceList = new List<LoginHistory>();

                switch (_comboBox.SelectedIndex)
                {
                    case 0:
                        var dataAll = context.LoginHistory.Select(x => x).ToList();
                        sourceList.AddRange(dataAll);
                        _history.ItemsSource = dataAll;
                        _someInfo.Text = "Общее количество входов: " + dataAll.Count();
                        break;
                    case 1:
                        var dataFalse = context.LoginHistory.Where(x => x.ValidAuth == false).Select(x => x).ToList();
                        sourceList.AddRange(dataFalse);
                        _history.ItemsSource = dataFalse;
                        _someInfo.Text = "Количество неудачных входов: " + dataFalse.Count();
                        break;
                    case 2:
                        var dataTrue = context.LoginHistory.Where(x => x.ValidAuth == true).Select(x => x).ToList();
                        sourceList.AddRange(dataTrue);
                        _history.ItemsSource = dataTrue;
                        _someInfo.Text = "Количество удачных входов: " + dataTrue.Count();
                        break;
                }
            }


           
        }

        private void _filter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            using (var context = new ForestMarathoneEntities())
            {
                var source = context.LoginHistory.Where(x => x.Login.Contains(_filter.Text)).ToList();
                sourceList = new List<LoginHistory>();
                sourceList.AddRange(source);
                _history.ItemsSource = source;

            }
        }
    }
}
