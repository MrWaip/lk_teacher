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
    class ChangeEventViewModel : BindableBase
    {
        private ChangeEventModel _CEModel;

        public string TitleEvent
        {
            get
            {
                return _CEModel.TitleEvent;
            }
            set
            {
                _CEModel.TitleEvent = value;
            }
        }

        public string DayOfWeekEvent
        {
            get { return _CEModel.DayOfWeekEvent; }
        }

        public string DateEvent
        {
            get { return _CEModel.DateEvent.ToString("dd/MM/yyyy - HH:mm"); }
        }

        public int TypeOfEvent
        {
            get
            {
                return _CEModel.TypeOfEvent;
            }
            set { _CEModel.TypeOfEvent = value; }
        }

        public string DescriptionEvent
        {
            get { return _CEModel.DescriptionEvent; }
            set
            {
                _CEModel.DescriptionEvent = value;
            }
        }

        public bool CanSave
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

        private RelayCommand _SaveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveCommand ??
                  (_SaveCommand = new RelayCommand(obj =>
                  {
                      if (CanSave)
                      {
                          _CEModel.SaveChanges();
                      }
                      (obj as Window).Close();
                  }));
            }
        }

        public ChangeEventViewModel(int id_event)
        {
            _CEModel = new ChangeEventModel(id_event);
            _CEModel.PropertyChanged += ModelPropertyChanged;
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
