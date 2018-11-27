using LK_Teacher.Event;
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
using System.Windows;
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

        public string TitleString
        {
            get
            {
                return _EGModel.TitleString;
            }
        }

        private string _TimeToEnd;
        public string TimeToEnd
        {
            get { return _TimeToEnd; }
            set { SetProperty(ref _TimeToEnd, value); }
        }

        private string _TitleClock;
        public string TitleClock
        {
            get { return _TitleClock; }
            set { SetProperty(ref _TitleClock, value); }
        }

        private Visibility _LabelCloclVisibility = Visibility.Collapsed;
        public Visibility LabelCloclVisibility
        {
            get { return _LabelCloclVisibility; }
            set { SetProperty(ref _LabelCloclVisibility, value); }
        }

        public EventGridViewModel(DateTime moday)
        {
            EventTimer.Instance.Subscribe(TimerUpdater);
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

        private void TimerUpdater(EventTimerArgs arg)
        {
            switch (arg.Status)
            {
                case "day_end":
                    TitleClock = "Рабочий день закончен";
                    LabelCloclVisibility = Visibility.Collapsed;
                    TimeToEnd = "";
                    break;
                case "pause":
                    TitleClock = "Перерыв";
                    LabelCloclVisibility = Visibility.Collapsed;
                    TimeToEnd = "";
                    break;
                case "event_comming":
                    TitleClock = "Время до конца:";
                    LabelCloclVisibility = Visibility.Visible;
                    TimeToEnd = arg.TimeToEnd.ToString(@"hh\:mm\:ss");
                    break;
            }

            //TimeToEnd = arg.TimeToEnd.ToString(@"hh\:mm\:ss");
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
