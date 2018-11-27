using LK_Teacher.Modules.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LK_Teacher.Modules.ViewModel
{

    class ProfileViewModel : BindableBase
    {
        private ProfileModel _PModel;

        public string Email
        {
            get { return _PModel.Email; }
            set { _PModel.Email = value; }
        }
        public string FName
        {
            get { return _PModel.FName; }
            set { _PModel.FName = value; }
        }
        public string LName
        {
            get { return _PModel.LName; }
            set { _PModel.LName = value; }
        }
        public string MName
        {
            get { return _PModel.MName; }
            set { _PModel.MName = value; }
        }
        public string Direction
        {
            get { return _PModel.Direction; }
            set { _PModel.Direction = value; }
        }
        public string Quote
        {
            get
            {
                if (_PModel.Quote == "")
                {
                    return "[Заполните цитату]";
                }
                else return _PModel.Quote;
            }
        }
        public string FullName
        {
            get
            {
                return _PModel.FullName;
            }
        }

        public string PhoneNumber
        {
            get { return _PModel.PhoneNumber; }
            set { _PModel.PhoneNumber = value; }
        }
        public string DateBirth
        {
            get
            {
                if (_PModel.DateBirth.Year != 1)
                {
                    return _PModel.DateBirth.ToShortDateString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                _PModel.DateBirth = DateTime.Parse(value);
            }
        }

        public ObservableCollection<CheckBox> SubjectList
        {
            get
            {
               return _PModel.SubjectList;
            }
        }


        public ProfileViewModel()
        {
            _PModel = new ProfileModel();
            _PModel.PropertyChanged += ModelPropertyChanged;
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
