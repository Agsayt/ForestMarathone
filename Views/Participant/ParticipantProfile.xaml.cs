using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
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
using Telerik.Windows.Input;

namespace ForestMarathone.Views.Participant
{

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


    /// <summary>
    /// Логика взаимодействия для ParticipantProfile.xaml
    /// </summary>
    /// 
    public partial class ParticipantProfile : Window
    {

        Users user;

        //Если создание
        public ParticipantProfile(Users user)
        {
            InitializeComponent();
            this.user = user;
            _userId.Text = user.UserId.ToString();
            _login.Text = user.Login.ToString();
            _email.Text = user.Email.ToString();

            using(var context = new ForestMarathoneEntities())
            {
                _country.ItemsSource = context.Countries.ToList();
                _country.SelectedValuePath = "Id";
                _country.SelectedIndex = 0;
            }
            _backButton.Visibility = Visibility.Collapsed;
            _mainButton.Content = "Создать профиль";
            _mainButton.Click += CreateProfile;
            _birthDate.DisplayDateEnd = DateTime.Now.Subtract(new TimeSpan(365*10, 0, 0,0));
        }

        private void CreateProfile(object sender, RoutedEventArgs e)
        {
            if (!CreateValidation())
                return;

            if (!PasswordValidation())
                return;

            var participant = new ForestMarathone.Participant();

            
            participant.UserId = user.UserId;
            participant.FirstName = _firstName.Text;
            participant.MiddleName = _middleName.Text;
            participant.LastName = _lastName.Text;

            var sexFlag = Convert.ToInt32((_sex.SelectedItem as ComboBoxItem).Tag) == 1 ? true : false;

            participant.Sex = sexFlag;
            participant.Birthday = (DateTime)_birthDate.SelectedDate;
            participant.CountryId = (int)_country.SelectedValue;
            participant.Phone = _phoneNumber.Text;
            participant.Wallet = 0;

            using (var context = new ForestMarathoneEntities())
            {
                participant.Id = context.Participant.Max(x => x.Id) + 1;
                context.Participant.Add(participant);
                context.SaveChanges();
            }

            MessageBox.Show("Профиль успешно создан!");
            ParticipantMenu pm = new ParticipantMenu(participant);
            pm.Show();
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
                if (_currentPassword != null && !String.IsNullOrWhiteSpace(_oldPassword.Password))
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

            if (_birthDate.SelectedDate == null)
            {
                errorMessage = "\n- Необходимо указать дату рождения";
                IsValid = false;
            }

            if (!_phoneNumber.IsMaskCompleted)
            {
                errorMessage = "\n- Не заполнен номер телефона";
                IsValid = false;
            }

            if (!IsValid)
                MessageBox.Show("При создании профиля произошла ошибка. Причины:" + errorMessage, "Ошибка!");

            return IsValid;
        }


        ForestMarathone.Participant participant;

        //Если редактирование
        public ParticipantProfile(ForestMarathone.Participant participant)
        {
            InitializeComponent();
            this.participant = participant;
            _mainButton.Content = "Обновить профиль";
            _mainButton.Click += UpdateProfile;

            _userId.Text = participant.UserId.ToString();

            using (var context = new ForestMarathoneEntities())
            {
                var user = context.Users.Where(x => x.UserId == participant.UserId).FirstOrDefault();
                _login.Text = user.Login;
                _email.Text = user.Email;
            }

            _birthDate.DisplayDateEnd = DateTime.Now.Subtract(new TimeSpan(365 * 10, 0, 0, 0));
            _firstName.Text = participant.FirstName;
            _middleName.Text = participant.MiddleName;
            _lastName.Text = participant.LastName;
            _sex.SelectedIndex = participant.Sex == true ? 1 : 0;
            _money.Text = participant.Wallet.ToString();
            _phoneNumber.Text = participant.Phone.Substring(3, 3) + participant.Phone.Substring(8, 3) + participant.Phone.Substring(12, 2) + participant.Phone.Substring(15, 2);

            _birthDate.SelectedDate = participant.Birthday;

            int age = DateTime.Now.Year - participant.Birthday.Year;

            if (DateTime.Now.Month < participant.Birthday.Month || (DateTime.Now.Month == participant.Birthday.Month && DateTime.Now.Day < participant.Birthday.Day))
                age--;

            _lastNameText.Text = _lastNameText.Text.Substring(0, participant.LastName.Length - 2) + $"({age} лет):";


            using (var context = new ForestMarathoneEntities())
            {
                _country.ItemsSource = context.Countries.ToList();
                _country.SelectedValuePath = "Id";
                _country.SelectedIndex = participant.CountryId-1;
            }

            
        }

        private void UpdateProfile(object sender, RoutedEventArgs e)
        {
            if (!CreateValidation())
                return;

            if (!PasswordValidation())
                return;


           

            using (var context = new ForestMarathoneEntities())
            {
                var participantToChange = context.Participant.Where(x => x.Id == participant.Id).FirstOrDefault();

                participantToChange.FirstName = _firstName.Text;
                participantToChange.MiddleName = _middleName.Text;
                participantToChange.LastName = _lastName.Text;

                var sexFlag = Convert.ToInt32((_sex.SelectedItem as ComboBoxItem).Tag) == 1 ? true : false;

                participantToChange.Sex = sexFlag;
                participantToChange.Birthday = (DateTime)_birthDate.SelectedDate;
                participantToChange.CountryId = (int)_country.SelectedValue;
                participantToChange.Phone = _phoneNumber.Text;
                participantToChange.Wallet = 0;
                context.SaveChanges();
            }

            MessageBox.Show("Профиль успешно создан!");
            ParticipantMenu pm = new ParticipantMenu(participant);
            pm.Show();
            this.Close();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadAvatar(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg)|*.jpg|All files (*.*)|*.*";
            ofd.Title = "Загрузка аватара";
            if(ofd.ShowDialog() == true)
            {
                _profilePicture.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
