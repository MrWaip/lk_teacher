using LK_Teacher.Modules.Models;
using LK_Teacher.Utility;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LK_Teacher.Modules.ViewModel
{
    class AddEventViewModel :BindableBase
    {
        private AddEventModel _AEModel;

        public string TitleEvent
        {
            get
            {
                return _AEModel.TitleEvent;
            }
            set
            {
                _AEModel.TitleEvent = value;
            }
        }

        public string DayOfWeekEvent
        {
            get { return _AEModel.DayOfWeekEvent; }
        }

        public string DateEvent
        {
            get { return _AEModel.DateEvent.ToString("dd/MM/yyyy - HH:mm"); }
        }

        public int TypeOfEvent
        {
            get
            {
                return _AEModel.TypeOfEvent;
            }
            set { _AEModel.TypeOfEvent = value; }
        }

        public string DescriptionEvent
        {
            get { return _AEModel.DescriptionEvent; }
            set
            {
                _AEModel.DescriptionEvent = value;
            }
        }

        public bool CanAdd
        {
            get
            {
                if (DescriptionEvent.Length >= 3 && TypeOfEvent != 0 && TitleEvent.Length >= 3)
                {
                    return true;
                }
                else return false;
            }
        }

        private RelayCommand _AddCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _AddCommand ??
                  (_AddCommand = new RelayCommand(obj =>
                  {
                      if (CanAdd)
                      {
                          _AEModel.AddEvent();
                      }
                      (obj as Window).Close();
                  }));
            }
        }

        public AddEventViewModel(DateTime date_event)
        {
            _AEModel = new AddEventModel(date_event);
            _AEModel.PropertyChanged += ModelPropertyChanged;
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
