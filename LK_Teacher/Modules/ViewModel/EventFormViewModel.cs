using LK_Teacher.Event;
using LK_Teacher.Modules.Models;
using LK_Teacher.Modules.Utility;
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
    class EventFormViewModel :BindableBase
    {
        private EventFormModel _EFModel;

        public Visibility Visibility
        {
            get
            {
                if (_EFModel.IsSetEvent)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            }
        }

        public string TitleEvent
        {
            get { return _EFModel.TitleEvent;
 }
        }

        public string DayOfWeekEvent
        {
            get { return _EFModel.DayOfWeekEvent; }
        }

        public string DateEvent
        {
            get { return _EFModel.DateEvent.ToString("dd/MM/yyyy - HH:mm"); }
        }

        public string TypeOfEvent
        {
            get
            {
                return 
UtilFunctions.GetTypeEvent(_EFModel.TypeOfEvent);
            }
        }

        public string DescriptionEvent
        {
            get { return _EFModel.DescriptionEvent; }
        }

        public EventFormViewModel()
        {
            _EFModel = new EventFormModel();
            _EFModel.PropertyChanged += ModelPropertyChanged;
        }

        private RelayCommand _ChangeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                return _ChangeCommand ??
                  (_ChangeCommand = new RelayCommand(obj =>
                  {
                      _EFModel.ChangeEvent();
                  }));
            }
        }

        private RelayCommand _DeleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _DeleteCommand ??
                  (_DeleteCommand = new RelayCommand(obj =>
                  {
                      _EFModel.DeleteEvent();
                  }));
            }
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
