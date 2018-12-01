using LK_Teacher.Modules.Models;
using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace LK_Teacher.Modules.ViewModel
{
    class RegistrationViewModel : BindableBase, IDataErrorInfo
    {
        private RegistrationModel _RModel;

        //Флаги валидации
        public bool IsValidPassword { get; set; }
        private bool IsValidEmail { get; set; }
        private bool IsValidLogin { get; set; }
        private bool IsValidFname { get; set; }
        private bool IsValidLname { get; set; }
        private bool IsValidMname { get; set; }
        private bool IsValidPhone { get; set; }

        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set
            {
                SetProperty(ref _Email, value);
            }
        }

        private string _Login = string.Empty;
        public string Login
        {
            get { return _Login; }
            set { SetProperty(ref _Login, value); }
        }

        private string _Fname = string.Empty;
        public string Fname
        {
            get { return _Fname; }
            set { SetProperty(ref _Fname, value); }
        }

        private string _Lname = string.Empty;
        public string Lname
        {
            get { return _Lname; }
            set { SetProperty(ref _Lname, value); }
        }

        private string _Mname = string.Empty;
        public string Mname
        {
            get { return _Mname; }
            set { SetProperty(ref _Mname, value); }
        }

        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get
            {
                if (_PhoneNumber == string.Empty)
                {
                    return "+7";
                }
                else
                {
                    return _PhoneNumber;
                }
            }
            set
            {
                if (value.IndexOf("+7") == 0)
                {
                    _PhoneNumber = value;
                }
                else
                {
                    _PhoneNumber = "+7" + value;
                }
            }
        }

        public bool IsValidInput
        {
            get
            {
                if (IsValidEmail && IsValidLogin && IsValidFname && IsValidLname && IsValidMname && IsValidPhone && IsValidPassword)
                {
                    return true;
                }
                return false;
            }
        }

        public RegistrationViewModel()
        {
            _RModel = new RegistrationModel();
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Email":
                        if (!UtilFunctions.IsValidEmail(Email))
                        {
                            error = "Введённые данные не корректны!";
                            IsValidEmail = false;
                        }
                        else
                        {
                            if (DBApi.HasSameField("teachers", "email_teacher", Email))
                            {
                                error = "Этот электронный адрес уже занят!";
                                IsValidEmail = false;
                            }
                            else
                                IsValidEmail = true;
                        }
                        break;

                    case "Login":
                        if (Login.Length < 3)
                        {
                            error = "Введённые данные не корректны";
                            IsValidLogin = false;
                        }
                        else
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(Login, "^[a-zA-Z0-9_]*$"))
                            {
                                IsValidLogin = false;
                                error = "Это поле может содержать только символы латинского алфавита!";
                            }
                            else
                            {
                                if (DBApi.HasSameField("teachers", "login_teacher", Login))
                                {
                                    IsValidLogin = false;
                                    error = "Это имя уже занято";
                                }
                                else
                                    IsValidLogin = true;
                            }
                        }
                        break;

                    case "Fname":
                        if (Fname.Length < 3)
                        {
                            error = "Введённые данные не корректны";
                            IsValidFname = false;
                        }
                        else IsValidFname = true;
                        break;
                    case "Lname":
                        if (Lname.Length < 3)
                        {
                            error = "Введённые данные не корректны";
                            IsValidLname = false;
                        }
                        else IsValidLname = true;
                        break;
                    case "Mname":
                        if (Mname.Length < 3)
                        {
                            error = "Введённые данные не корректны";
                            IsValidMname = false;
                        }
                        else IsValidMname = true;
                        break;
                    case "PhoneNumber":
                        if (PhoneNumber.Length == 12)
                        {
                            if (!UtilFunctions.IsValidPhoneNumber("+7" + PhoneNumber))
                            {
                                error = "Введённые данные не корректны";
                                IsValidPhone = false;
                            }
                            else
                            {
                                string formatNumber = UtilFunctions.FormatPhoneNumber(PhoneNumber);

                                if (DBApi.HasSameField("teachers", "phone_number_teacher", formatNumber))
                                {
                                    error = "Этот номер уже занят!";
                                    IsValidPhone = false;
                                }
                                else IsValidPhone = true;
                            }
                        }
                        else
                        {
                            error = "Введённые данные не корректны";
                            IsValidPhone = false;
                        }
                        break;
                }

                RaisePropertyChanged("IsValidInput");

                return error;
            }
        }

        public void CheckValid()
        {
            RaisePropertyChanged("IsValidInput");
        }

        public string Error
        {
            get
            {
                return "";
            }
        }


    }
}
