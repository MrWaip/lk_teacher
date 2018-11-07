using LK_Teacher.Moduls.API;
using System;
using System.Collections.Generic;
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

namespace LK_Teacher.Moduls.Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        //Флаги валидации

        private bool EmailValid = false;
        private bool LoginValid = false;
        private bool PassValid = false;
        private bool PhoneValid = false;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            DataBaseApi.Registration(EmailInput.Text, LoginInput.Text, PasswordInput.Password, FnameInput.Text, LnameInput.Text,MnameInput.Text, PhoneNumberInput.Tag.ToString());
            this.Close();
        }

        //Обработчики на валидацию


        //Валидация почты

        private void EmailInput_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
        }

        private void EmailInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UtilityApi.IsValidEmail(EmailInput.Text))
            {
                if (DataBaseApi.HasSameField("teachers","email_teacher",EmailInput.Text))
                {
                    EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    EmailInput.ToolTip = new ToolTip().Content = "Этот электронный адресс уже занят!";
                    EmailValid = false;
                }
                else
                {
                    EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    EmailInput.ToolTip = null;
                    EmailValid = true;
                }
                

            }
            else
            {
                EmailInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                EmailInput.ToolTip = new ToolTip().Content = "Неверный формат Email \nПример: adress@mail.domen";
                EmailValid = false;
            }

            CheckValid();
        }

        //Валидация Логина

        private void LoginInput_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
        }

        private void LoginInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginInput.Text.Length >= 3)
            {
                if (DataBaseApi.HasSameField("teachers", "login_teacher", LoginInput.Text))
                {
                    LoginInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    LoginInput.ToolTip = new ToolTip().Content = "Это имя уже занято!";
                    LoginValid = false;
                }
                else
                {
                    LoginInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    LoginInput.ToolTip = null;
                    LoginValid = true;
                }
            }
            else
            {
                LoginInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                LoginInput.ToolTip = "Минимальная длина строки: 3!";
                LoginValid = false;
            }
            CheckValid();
        }

        private void LoginInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid();
        }


        //Валидация ПАРОЛЯ

        private void PasswordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordInput.Password.Length >= 4)
            {
                PasswordInput.ToolTip = null;
                PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");

                if (PasswordInput.Password == PasswordInputRepeat.Password)
                {
                    PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    PasswordInput.ToolTip = null;
                    PasswordInputRepeat.ToolTip = null;
                    PassValid = true;
                }
                if (PasswordInput.Password != PasswordInputRepeat.Password && PasswordInputRepeat.Password.Length != 0)
                {
                    PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInput.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                    PasswordInputRepeat.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                    PassValid = false;
                }
            }
            else
            {
                PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                PasswordInput.ToolTip = new ToolTip().Content = "Минимальная длина пароля: 4 символа!";
                PassValid = false;
            }
            CheckValid();
        }

        private void PasswordInputRepeat_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordInputRepeat.ToolTip = null;
            PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");

            if (PasswordInputRepeat.Password.Length >= 4)
            {
                if (PasswordInput.Password == PasswordInputRepeat.Password)
                {
                    PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    PasswordInput.ToolTip = null;
                    PasswordInputRepeat.ToolTip = null;
                    PassValid = true;
                }
                else
                {
                    PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInputRepeat.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                    PasswordInput.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                    PassValid = false;
                }
            }
            else
            {
                PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                PasswordInputRepeat.ToolTip = new ToolTip().Content = "Минимальная длина пароля: 4 символа!";
                PassValid = false;
            }
            CheckValid();
        }


        //Проверяем эти поля на мин символы - от 3х

        private void MnameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid();
        }

        private void FnameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid();
        }

        private void LnameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid();
        }

        //Валидация номера телефона

        private void PhoneNumberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UtilityApi.IsValidPhoneNumber("+7"+PhoneNumberInput.Text))
            {
                string validNumber = UtilityApi.FormatPhoneNumber("+7"+PhoneNumberInput.Text);
                PhoneNumberInput.Tag = validNumber;

                if (DataBaseApi.HasSameField("teachers", "phone_number_teacher", validNumber))
                {
                    PhoneNumberInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    NumberPrefix.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PhoneNumberInput.ToolTip = new ToolTip().Content = "Этот номер уже зарегистрирован!";
                    PhoneValid = false;
                }
                else
                {
                    PhoneNumberInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                    PhoneNumberInput.ToolTip = null;
                    PhoneValid = true;
                }
            }
            else
            {
                PhoneNumberInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                NumberPrefix.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                PhoneNumberInput.ToolTip = "Неверный формат номера!\n Пример: +7 123 456 78 90 ";
                PhoneValid = false;
            }
            CheckValid();
        }

        private void PhoneNumberInput_GotFocus(object sender, RoutedEventArgs e)
        {
            PhoneNumberInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
            NumberPrefix.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
        }

        //Метод общей валидации

        private void CheckValid()
        {
            if (EmailValid &&
                LoginValid &&
                PassValid &&
                PhoneValid &&
                FnameInput.Text.Length >= 3 &&
                LnameInput.Text.Length >= 3 &&
                MnameInput.Text.Length >= 3
                )
            {
                SignUp.IsEnabled = true;
            }
            else
            {
                SignUp.IsEnabled = false;
            }
        }
    }
}
