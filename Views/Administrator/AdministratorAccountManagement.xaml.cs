using ForestMarathone.Models;
using ForestMarathone.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForestMarathone.Views.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdministratorAccountManagement.xaml
    /// </summary>
    public partial class AdministratorAccountManagement : Window
    {
        
        public AdministratorAccountManagement()
        {
            InitializeComponent();

            using (var context = new ForestMarathoneEntities())
            {
                _rolesCb.ItemsSource = context.Roles
                    .Where(x => x.RoleId != Role.Administrator)
                    .ToList();

            }


            DataContext = new GetUserBasic();
        }


        public void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void _newButton_Click(object sender, RoutedEventArgs e)
        {
            VisibilityStatusChanger();
            _rolesCb.SelectedIndex = 0;
            _grid.IsEnabled = false;
            _grid.SelectedIndex = -1;
        }

        private void _cancelButton_Click(object sender, RoutedEventArgs e)
        {
            VisibilityStatusChanger();
            _grid.IsEnabled = true;
            _rolesCb.SelectedItem = null;
        }

        private void VisibilityStatusChanger()
        {
            _accGrid.IsEnabled = !_accGrid.IsEnabled;

            if (_cancelButton.IsVisible)
                _cancelButton.Visibility = Visibility.Collapsed;
            else
                _cancelButton.Visibility = Visibility.Visible;

            if (_addButton.IsVisible)
                _addButton.Visibility = Visibility.Collapsed;
            else
                _addButton.Visibility = Visibility.Visible;


            if (_passConfirmationText.IsVisible)
                _passConfirmationText.Visibility = Visibility.Collapsed;
            else
                _passConfirmationText.Visibility = Visibility.Visible;

            if (_passConfirmationBox.IsVisible)
                _passConfirmationBox.Visibility = Visibility.Collapsed;
            else
                _passConfirmationBox.Visibility = Visibility.Visible;

            _loginBox.Text = String.Empty;
            _emailBox.Text = String.Empty;
            _passBox.Text = String.Empty;
            _passConfirmationBox.Text = String.Empty;
        }

        private void _addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!EmptyCheck())
            {
                return;
            }

            if (!CreateValidation())
            {
                return;
            }


            var newUser = new Users()
            {
                Login = _loginBox.Text,
                Email = _emailBox.Text,
                Password = _passBox.Text,
                RoleId = (_rolesCb.SelectedItem as Roles).RoleId,
                UserStatusId = UStatus.Activated
            };

            using (var context = new ForestMarathoneEntities())
            {
                newUser.UserId = context.Users.Max(x => x.UserId) + 1;
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            MessageBox.Show($"Пользователь {newUser.Login} успешно создан!", "Результат операции");
            DataContext = new GetUserBasic();
            _grid.UpdateLayout();
        }

        #region validation
        private bool EmptyCheck()
        {
            var IsValid = true;
            var errorMessage = String.Empty;

            if (String.IsNullOrWhiteSpace(_loginBox.Text))
            {
                errorMessage += "\n- Поле логин пустое";
                IsValid = false;
            }

            if(String.IsNullOrWhiteSpace(_emailBox.Text))
            {
                errorMessage += "\n- Поле почты пустое";
                IsValid = false;
            }

            if (String.IsNullOrWhiteSpace(_passBox.Text))
            {
                errorMessage += "\n- Поле пароля пустое";
                IsValid = false;
            }

            if(String.IsNullOrWhiteSpace(_passConfirmationBox.Text))
            {
                errorMessage += "\n- Поле подтверждения пароля пустое";
                IsValid = false;
            }

            if (!IsValid)
                MessageBox.Show("При попытке создать нового пользователя произошла ошибка. Причина:" + errorMessage, "Ошибка");

            return IsValid;
        }
        private bool CreateValidation()
        {
            string errorMessage = String.Empty;

            var IsValid = true;

            if (_loginBox.Text.Length < 6)
            {
                errorMessage += "\n- Длина логина должна быть более 6 символов";
                IsValid = false;
            }

            if (!UserNameValidation())
            {
                errorMessage += "\n- Имя пользователя занято, придумайте новый логин";
                IsValid = false;
            }

            if (!EmailValidation())
            {
                errorMessage += "\n- Почта не соответствует формату";
                IsValid = false;
            }

            if (!EmailConfirmation())
            {
                errorMessage += "\n- Указанная почта уже зарегестрирована в системе";
                IsValid = false;
            }

            if (!PasswordValidation(out string message))
            {
                errorMessage = message;
                IsValid = false;
            }

            if (!IsValid)
                MessageBox.Show("При попытке создать пользователя произошла ошибка. Причины:" + errorMessage, "Ошибка!");

            return IsValid;
        }
        private bool EmailConfirmation()
        {
            bool IsValid = true;
            using (var context = new ForestMarathoneEntities())
            {
                IsValid = context.Users.Where(x => x.Email == _emailBox.Text).Count() == 0;
            }

            return IsValid;
        }
        private bool PasswordValidation(out string errorMessage)
        {
            var IsValid = true;
            errorMessage = String.Empty;
            if(!_passBox.Text.Equals(_passConfirmationBox.Text))
            {
                errorMessage += "\n- Пароль и подтверждающий пароль не совпадают";
                IsValid = false;
            }

            if (_passBox.Text.Length < 6)
            {
                errorMessage += "\n- Длина пароля менее 6 символов";
                IsValid = false;
            }

            if (!_passBox.Text.Any(Char.IsDigit))
            {
                errorMessage += "\n- Пароль должен содержать как минимум одну цифру";
                IsValid = false;
            }

            
            return IsValid;
        }
        private bool EmailValidation()
        {            
            try
            {                
                var email = Regex.Replace(_emailBox.Text, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                
                string DomainMapper(Match match)
                {
                    
                    var idn = new IdnMapping();

                   
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(_emailBox.Text,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private bool UserNameValidation()
        {
            bool IsValid = true;
            using (var context = new ForestMarathoneEntities())
            {
                IsValid = context.Users.Where(x => x.Login == _loginBox.Text).Count() == 0;
            }

            return IsValid;
        }
        #endregion

        private void _grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var user = (sender as DataGrid).CurrentItem as UserBasic;

            if (user == null)
                return;

            _loginBox.Text = user.Login;
            _emailBox.Text = user.email;
            _passBox.Text = user.Password;

            var list = _rolesCb.Items.SourceCollection;
            var ser = list.OfType<Roles>().FirstOrDefault(x => x.RoleId == user.RoleId);
            foreach (var item in list)
            {
                var obj = item.GetType();
            }      

            _rolesCb.SelectedItem = ser;
        }
    }
}
