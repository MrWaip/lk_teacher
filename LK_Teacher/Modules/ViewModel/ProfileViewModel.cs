using LK_Teacher.Modules.Models;
using LK_Teacher.Utility;
using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            set
            {
                _PModel.Quote = value;
            }
        }
        public string ImagePath
        {
            get
            {
                if (_PModel.ImagePath != "")
                {
                    return _PModel.ImagePath;
                }
                else return @"\Assets\Images\img_avatar.png";
            }
            set
            {
                _PModel.ImagePath = value;
            }
        }
        public string FullName
        {
            get
            {
                return _PModel.FullName;
            }
        }
        public string Education
        {
            get
            {
                return _PModel.Education;
            }
            set
            {
                _PModel.Education = value;
            }
        }

        public string PhoneNumber
        {
            get { return _PModel.PhoneNumber; }
            set { _PModel.PhoneNumber = value; }
        }
        public DateTime DateBirth
        {
            get
            {
                return _PModel.DateBirth;

            }
            set
            {
                _PModel.DateBirth = value;
            }
        }

        public ObservableCollection<CheckBox> SubjectList
        {
            get
            {
               return _PModel.SubjectList;
            }
        }
        public ObservableCollection<CheckBox> DirectionList
        {
            get
            {
               return _PModel.DirectionList;
            }
        }

        public ProfileViewModel()
        {
            _PModel = new ProfileModel();
            _PModel.PropertyChanged += ModelPropertyChanged;
        }

        private RelayCommand _SaveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveCommand ??
                  (_SaveCommand = new RelayCommand(obj =>
                  {
                      _PModel.SaveChanges();
                  }));
            }
        }

        private RelayCommand _AddNewPhoto;
        public RelayCommand AddNewPhoto
        {
            get
            {
                return _AddNewPhoto ??
                  (_AddNewPhoto = new RelayCommand(obj =>
                  {
                      _PModel.NewPhoto();
                  }));
            }
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
