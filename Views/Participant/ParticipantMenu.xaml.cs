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

namespace ForestMarathone.Views.Participant
{
    /// <summary>
    /// Логика взаимодействия для ParticipantMenu.xaml
    /// </summary>
    public partial class ParticipantMenu : Window
    {
        Users user;
        ForestMarathone.Participant participant;

        public ParticipantMenu(ForestMarathone.Participant participant)
        {
            InitializeComponent();
            this.participant = participant;
        }

        public ParticipantMenu(ForestMarathone.Users user)
        {
            InitializeComponent();
            this.user = user;
            CreateCanvas();            
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
            button.Style = (Style) FindResource("TabButtonFirst");
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
            ParticipantProfile pp = new ParticipantProfile(user);
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
            ParticipantProfile pp = new ParticipantProfile(participant);
            this.Hide();
            pp.ShowDialog();
            this.Show();
        }
    }
}
