﻿using LK_Teacher.Moduls.API;
using LK_Teacher.Moduls.Registration;
using LK_Teacher.Moduls.Settings;
using System;
using System.Collections;
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

namespace LK_Teacher.Moduls.Authorization
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationForm.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        public Hashtable DataUser { get; private set; }

        public AuthorizationForm()
        {
            InitializeComponent();

            string connstr = $"server={Properties.Settings.Default.ServerHost};" +
                $"port={Properties.Settings.Default.ServerPort};" +
                $"user={Properties.Settings.Default.Username};" +
                $"database={Properties.Settings.Default.Database};" +
                $"password={Properties.Settings.Default.Password};" +
                $"SslMode=none;";

            DataBaseApi.InitializeApi(connstr);

            if (!DataBaseApi.IsConnection)
            {
                Environment.Exit(0);
            }

            //Чекаем remember me
            if (Properties.Settings.Default.RememberMode)
            {
                RememberMe.IsChecked = true;
                EmailInput.Text = Properties.Settings.Default.CacheEmail;
            }
        }

        private void CheckValid()
        {
            if (UtilityApi.IsValidEmail(EmailInput.Text) && PasswordInput.Password.Length >= 4)
            {
                SignIn.IsEnabled = true;
            }
            else
            {
                SignIn.IsEnabled = false;
            }
        }

        //Обработчики на валидность
        private void EmailInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UtilityApi.IsValidEmail(EmailInput.Text))
            {
                EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                EmailInput.ToolTip = null;
            }
            else
            {
                EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                EmailInput.ToolTip = new ToolTip().Content = "Неверный формат Email \nПример: adress@mail.domen";
            }

            CheckValid();
        }

        private void EmailInput_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckValid();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            ErrorBox.Visibility = Visibility.Hidden;
            DataUser = DataBaseApi.Authorization(EmailInput.Text, PasswordInput.Password);
            bool status_profile = Convert.ToBoolean(DataUser["status_profile_teacher"]);
            if (DataUser.Count != 0 && status_profile)
            {
                if ((bool)RememberMe.IsChecked)
                {
                    Properties.Settings.Default.RememberMode = true;
                    Properties.Settings.Default.CacheEmail = EmailInput.Text;
                    Properties.Settings.Default.Save();

                    DataBaseApi.EMAIL_TEACHER = EmailInput.Text;
                    DataBaseApi.ID_TEACHER = DataUser["id_teacher"].ToString();
                }
                this.Close();
            }

            if (DataUser.Count == 0)
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorMesage.Content = "Неверный адрес электронной почты или пароль!";
            }
            if (DataUser.Count != 0 && !Convert.ToBoolean(DataUser["status_profile_teacher"]))
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorMesage.Content = "Этот профиль не подтвержден администратором!";
                DataUser.Clear();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (DataUser != null && DataUser.Count != 0)
            {
                Application.Current.MainWindow = new MainWindow(DataUser);
                Application.Current.MainWindow.Show();
            }
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
    }
}
