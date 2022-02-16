using Microsoft.Win32;
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
    /// Логика взаимодействия для SponsorProfile.xaml
    /// </summary>
    public partial class SponsorProfile : Window
    {
        private Sponsors sponsor;
        private Users user;

        public SponsorProfile(Users user)
        {
            InitializeComponent();
            this.user = user;
            _userId.Text = user.UserId.ToString();
            _login.Text = user.Login.ToString();
            _email.Text = user.Email.ToString();

            using (var context = new ForestMarathoneEntities())
            {
                _country.ItemsSource = context.Countries.ToList();
                _country.SelectedValuePath = "Id";
                _country.SelectedIndex = 0;
            }

            var yearList = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                yearList.Add(DateTime.Now.Year + i + 1);
            }

            _cardYear.ItemsSource = yearList.ToArray();

            _backButton.Visibility = Visibility.Collapsed;
            _mainButton.Content = "Создать профиль";
            _mainButton.Click += CreateProfile;
        }

        private void CreateProfile(object sender, RoutedEventArgs e)
        {
            if (!CreateValidation())
                return;

            if (!PasswordValidation())
                return;

            var sponsor = new ForestMarathone.Sponsors();


            sponsor.UserId = user.UserId;
            sponsor.FirstName = _firstName.Text;
            sponsor.MiddleName = _middleName.Text;
            sponsor.LastName = _lastName.Text;
            sponsor.TotalSum = 0;
            sponsor.CardValidYear = Convert.ToInt32((_cardYear.SelectedItem));
            sponsor.CardValidMonth = Convert.ToInt32((_cardMonth.SelectedItem as ComboBoxItem).Tag);
            sponsor.CardNumber = Convert.ToDecimal(_cardNumber.Text);


            sponsor.CountryId = (int)_country.SelectedValue;

            using (var context = new ForestMarathoneEntities())
            {
                sponsor.Id = context.Sponsors.Max(x => x.Id) + 1;
                context.Sponsors.Add(sponsor);

                context.SaveChanges();
            }

            MessageBox.Show("Профиль успешно создан!");
            SponsorMenu sm = new SponsorMenu(sponsor);
            sm.Show();
            this.Close();

            //MemoryStream ms = new MemoryStream();
            //BitmapSource bS = (BitmapSource)_profilePicture.Source;
            //ms.
            //_profilePicture. .Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        }

        private bool PasswordValidation()
        {
            using (var context = new ForestMarathoneEntities())
            {
                if (_currentPassword.Password != null && !String.IsNullOrWhiteSpace(_oldPassword.Password))
                {
                    var correctPass = context.Users.Where(x => x.UserId == user.UserId).Select(x => x.Password).FirstOrDefault();
                    if (correctPass == _currentPassword.Password)
                    {
                        if (_oldPassword.Password != _oldPassConf.Password)
                            return false;

                        var userToChange = context.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
                        userToChange.Password = _oldPassword.Password;
                        context.SaveChanges();
                    }
                    else return false;
                }
                else return true;
            }

            return true;
        }

        private bool CreateValidation()
        {
            var errorMessage = String.Empty;
            var IsValid = true;

            if (string.IsNullOrWhiteSpace(_firstName.Text))
            {
                errorMessage = "\n- Поле имя не может быть пустым";
                IsValid = false;
            }


            if (string.IsNullOrWhiteSpace(_lastName.Text))
            {
                errorMessage = "\n- Поле фамилия не может быть пустым";
                IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(_email.Text))
            {
                errorMessage = "\n- Поле почты не может быть пустым";
                IsValid = false;
            }

           

            if (!IsValid)
                MessageBox.Show("При создании профиля произошла ошибка. Причины:" + errorMessage, "Ошибка!");

            return IsValid;
        }


        // On update profile
        public SponsorProfile(Sponsors sponsor)
        {
            InitializeComponent();
            using (var context = new ForestMarathoneEntities())
            {
                this.sponsor = context.Sponsors.Where(x => x.Id == sponsor.Id).FirstOrDefault();
            }
            _mainButton.Content = "Обновить профиль";
            _mainButton.Click += UpdateProfile;

            using (var context = new ForestMarathoneEntities())
            {
                var user = context.Users.Where( x => x.UserId == sponsor.UserId ).FirstOrDefault();
                _login.Text = user.Login;
                _email.Text = user.Email;
            }
            _userId.Text = sponsor.UserId.ToString();            
            _firstName.Text = sponsor.FirstName;
            _middleName.Text = sponsor.MiddleName;
            _lastName.Text = sponsor.LastName;
            _money.Text = sponsor.TotalSum.ToString();
            _cardMonth.Text = sponsor.CardValidMonth.ToString();
            _cardYear.Text = sponsor.CardValidYear.ToString();
            _cardNumber.Text = sponsor.CardNumber.ToString();


            var yearList = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                yearList.Add(DateTime.Now.Year + i + 1);
            }

            _cardYear.ItemsSource = yearList.ToArray();

            using (var context = new ForestMarathoneEntities())
            {
                _country.ItemsSource = context.Countries.ToList();
                _country.SelectedValuePath = "Id";
                _country.SelectedIndex = sponsor.CountryId - 1;
            }


        }

        private void UpdateProfile(object sender, RoutedEventArgs e)
        {
            if (!CreateValidation())
                return;

            if (!PasswordValidation())
                return;


            sponsor.UserId = user.UserId;
            sponsor.FirstName = _firstName.Text;
            sponsor.MiddleName = _middleName.Text;
            sponsor.LastName = _lastName.Text;
            sponsor.CardNumber = Convert.ToDecimal(_cardNumber.Text);
            sponsor.CardValidYear = Convert.ToInt32((_cardYear.SelectedItem as ComboBoxItem).Tag);
            sponsor.CardValidMonth = Convert.ToInt32((_cardMonth.SelectedItem as ComboBoxItem).Tag);

            sponsor.CountryId = (int)_country.SelectedValue;

            using (var context = new ForestMarathoneEntities())
            {
                context.Sponsors.Add(sponsor);

                context.SaveChanges();
            }

            MessageBox.Show("Профиль успешно создан!");
            SponsorMenu pm = new SponsorMenu(sponsor);
            pm.Show();
            this.Close();
        }

        private void LoadAvatar(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg)|*.jpg|All files (*.*)|*.*";
            ofd.Title = "Загрузка аватара";
            if (ofd.ShowDialog() == true)
            {
                _profilePicture.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class ImageDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            byte[] image = ((byte[])value).ToArray();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
