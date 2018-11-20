using LK_Teacher.Modules.Models;
using LK_Teacher.Modules.View;
using LK_Teacher.Utility;
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
    class EventGridViewModel : BindableBase
    {
        private EventGridModel _EGModel;

        private EventForm _EventForm;
        public EventForm EventForm
        {
            get { return _EventForm; }
            set
            {
                _EventForm = value;
                RaisePropertyChanged("EventForm");
            }
        }

        public ObservableCollection<Object> EventGrid
        {
            get { return _EGModel.EventGrid; }
        }

        public EventGridViewModel(DateTime moday)
        {
            _EGModel = new EventGridModel(moday);
            _EGModel.PropertyChanged += ModelPropertyChanged;
            EventForm = new EventForm();
        }

        /// <summary>
        /// Переход на следующий день NextDay
        /// </summary>
        private RelayCommand _NextDay;
        public RelayCommand NextDay
        {
            get
            {
                return _NextDay ??
                  (_NextDay = new RelayCommand(obj =>
                  {
                      _EGModel.ChangeWeek(7);
                  }));
            }
        }

        /// <summary>
        /// Переход на предыдущий день
        /// </summary>
        private RelayCommand _PreviousDay;
        public RelayCommand PreviousDay
        {
            get
            {
                return _PreviousDay ??
                  (_PreviousDay = new RelayCommand(obj =>
                  {
                      _EGModel.ChangeWeek(-7);
                  }));
            }
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
