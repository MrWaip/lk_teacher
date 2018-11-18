using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.Registration;
using LK_Teacher.Modules.Settings;
using LK_Teacher.Modules.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace LK_Teacher.Modules.View
{
    public partial class AuthorizationView : Window
    {
        private Hashtable _userData;

        public AuthorizationView()
        {

            InitializeComponent();

            string connstr = $"server={Properties.Settings.Default.ServerHost};" +
                $"port={Properties.Settings.Default.ServerPort};" +
                $"user={Properties.Settings.Default.Username};" +
                $"database={Properties.Settings.Default.Database};" +
                $"password={Properties.Settings.Default.Password};" +
                $"SslMode=none;";

            DBApi.InitializeApi(connstr);

            if (!DBApi.IsConnection)
            {
                Environment.Exit(0);
            }
        }

        private void CheckValid()
        {
            if (UtilFunctions.IsValidEmail(EmailInput.Text) && PasswordInput.Password.Length >= 4)
            {
                SignIn.IsEnabled = true;
            }
            else
            {
                SignIn.IsEnabled = false;
            }
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckValid();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            ErrorBox.Visibility = Visibility.Hidden;
            _userData = DBApi.Authorization(EmailInput.Text, PasswordInput.Password);
            string status_profile = (string)_userData["status_profile_teacher"];
            if (_userData.Count != 0 && (status_profile == DBApi.FIRST_TIME_PROFILE || status_profile == DBApi.ACTIVE_PROFILE))
            {
                if ((bool)RememberMe.IsChecked)
                {
                    Properties.Settings.Default.RememberMode = true;
                    Properties.Settings.Default.CacheEmail = EmailInput.Text;
                    Properties.Settings.Default.Save();

                    DBApi.EMAIL_TEACHER = EmailInput.Text;
                    DBApi.ID_TEACHER = _userData["id_teacher"].ToString();
                }
                this.Close();
            }

            if (_userData.Count == 0)
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorMesage.Content = "Неверный адрес электронной почты или пароль!";
            }
            if (_userData.Count != 0 && status_profile == DBApi.WAIT_PROFILE)
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorMesage.Content = "Этот профиль не подтвержден администратором!";
                _userData.Clear();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_userData != null && _userData.Count != 0)
            {
                var sv = new ShellView(_userData);

                Application.Current.MainWindow = sv;
                Application.Current.MainWindow.Show();
            }
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog();
        }

        private void EmailInput_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckValid();
        }

        private void SettingBt_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
    }
}
