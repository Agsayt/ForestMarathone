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

namespace ForestMarathone.Views.Sponsor
{
    /// <summary>
    /// Логика взаимодействия для SponsorMainMenu.xaml
    /// </summary>
    public partial class SponsorMenu : Window
    {
        private Sponsors sponsor;
        private Users user;

        public SponsorMenu(Users user)
        {
            InitializeComponent();
            this.user = user;
            CreateCanvas();
        }

        public SponsorMenu(Sponsors sponsor)
        {
            InitializeComponent();
            this.sponsor = sponsor;

        }


        private void CreateCanvas()
        {
            Grid canvas = new Grid();
            canvas.Width = MainGrid.Width;
            canvas.Height = MainGrid.Height;
            canvas.Background = new SolidColorBrush(Colors.White);


            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Приветствуем! Похоже, вы ещё не оформили профиль, что необходимо для дальнейшей работы с системой. Просим вас потратить пару минут на его заполнение, после чего вы сможете приступить к работе!";
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Margin = new Thickness(40);
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.FontSize = 20;

            Button button = new Button();
            button.Content = "Перейти";
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Bottom;
            button.Click += ToCreateProfile;
            button.Style = (Style)FindResource("TabButtonFirst");
            button.Width = 170;
            button.Height = 45;
            button.Margin = new Thickness(20);

            canvas.Children.Add(button);
            canvas.Children.Add(textBlock);


            Grid.SetColumnSpan(canvas, 3);
            Grid.SetRowSpan(canvas, 3);

            MainGrid.Children.Add(canvas);
            MainGrid.UpdateLayout();
        }

        private void ToCreateProfile(object sender, RoutedEventArgs e)
        {
            SponsorProfile pp = new SponsorProfile(user);
            pp.Show();
            this.Close();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void ToUpdateProfile(object sender, RoutedEventArgs e)
        {
            SponsorProfile pp = new SponsorProfile(sponsor);
            this.Hide();
            pp.ShowDialog();
            this.Show();
        }

        private void ToSponse(object sender, RoutedEventArgs e)
        {
            SponsorRunner sr = new SponsorRunner(sponsor);
            this.Hide();
            sr.ShowDialog();
            this.Show();
        }
    }

    
}
