using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.ViewModel
{
    public class AuthorizationModelView : BindableBase, IDataErrorInfo
    {
        //поля -----------------------------------------------------------------------------------

        private bool _rememberMe;

        //конструктор ----------------------------------------------------------------------------

        public AuthorizationModelView()
        {
            if (Properties.Settings.Default.RememberMode)
            {
                RememberMe = true;
                Email = Properties.Settings.Default.CacheEmail;
            }

        }

        //Свойства

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
                        }
                        break;
                }
                return error;
            }
        }

        public string Email { get; set; }

        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                _rememberMe = value;
                RaisePropertyChanged("RememberMe");
            }
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
