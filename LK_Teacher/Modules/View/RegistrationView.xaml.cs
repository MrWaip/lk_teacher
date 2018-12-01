using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LK_Teacher.Modules.View
{
    public partial class RegistrationView : Window
    {
        private RegistrationViewModel _RVM;

        public RegistrationView()
        {
            DataContext = _RVM = new RegistrationViewModel();
            
            InitializeComponent();

        }

        private void PasswordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordInput.Password.Length >= 4)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordInput.Password, "^[a-zA-Z0-9_]*$"))
                {
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInput.ToolTip = new ToolTip().Content = "Пароль может состоять только из символов латинского алфавита, цифр и символа: '_' .";
                    _RVM.IsValidPassword = false;
                }
                else
                {
                    PasswordInput.ToolTip = null;
                    PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");

                    if (PasswordInput.Password == PasswordInputRepeat.Password)
                    {
                        PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                        PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                        PasswordInput.ToolTip = null;
                        PasswordInputRepeat.ToolTip = null;
                        _RVM.IsValidPassword = true;
                    }
                    if (PasswordInput.Password != PasswordInputRepeat.Password && PasswordInputRepeat.Password.Length != 0)
                    {
                        PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                        PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                        PasswordInput.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                        PasswordInputRepeat.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                        _RVM.IsValidPassword = false;
                    }
                }
            }
            else
            {
                PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                PasswordInput.ToolTip = new ToolTip().Content = "Минимальная длина пароля: 4 символа!";
                _RVM.IsValidPassword = false;
            }
            _RVM.CheckValid();
        }

        private void PasswordInputRepeat_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordInputRepeat.ToolTip = null;
            PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");

            if (PasswordInputRepeat.Password.Length >= 4)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordInputRepeat.Password, "^[a-zA-Z0-9_]*$"))
                {
                    PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                    PasswordInputRepeat.ToolTip = new ToolTip().Content = "Пароль может состоять только из символов латинского алфавита, цифр и символа: '_' .";
                    _RVM.IsValidPassword = false;
                }
                else
                {
                    if (PasswordInput.Password == PasswordInputRepeat.Password)
                    {
                        PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                        PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF2C364D");
                        PasswordInput.ToolTip = null;
                        PasswordInputRepeat.ToolTip = null;
                        _RVM.IsValidPassword = true;
                    }
                    else
                    {
                        PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                        PasswordInput.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                        PasswordInputRepeat.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                        PasswordInput.ToolTip = new ToolTip().Content = "Пароли не совпадают!";
                        _RVM.IsValidPassword = false;
                    }
                }
            }
            else
            {
                PasswordInputRepeat.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC2020");
                PasswordInputRepeat.ToolTip = new ToolTip().Content = "Минимальная длина пароля: 4 символа!";
                _RVM.IsValidPassword = false;
            }
            _RVM.CheckValid();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (_RVM.IsValidInput)
            {
                try
                {
                    DBApi.Registration(_RVM.Email, _RVM.Login, PasswordInput.Password, _RVM.Fname, _RVM.Lname, _RVM.Mname, UtilFunctions.FormatPhoneNumber(_RVM.PhoneNumber));
                    this.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
