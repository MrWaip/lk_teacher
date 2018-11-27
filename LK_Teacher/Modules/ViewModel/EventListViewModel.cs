using LK_Teacher.Event;
using LK_Teacher.Modules.Models;
using LK_Teacher.Modules.Utility;
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

namespace LK_Teacher.Modules.ViewModel
{
    class EventListViewModel : BindableBase
    {
        private EventListModel _ELModel;

        //Форма для отображения данных
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

        /// <summary>
        /// Инициализируем VM
        /// </summary>
        /// <param name="day">День события</param>
        public EventListViewModel(DateTime day)
        {
            EventTimer.Instance.Subscribe(TimerUpdater);

            EventForm = new EventForm();
            _ELModel = new EventListModel();
            _ELModel.PropertyChanged += ModelpropertyChanged;
            _ELModel.InitializeEventList(day);
        }


        /// <summary>
        /// Заголовок tbMetaInfo в формате: Расписание на 24.04.2019 - Пятница
        /// </summary>
        public string TitleString
        {
            get
            {
                return _ELModel.TitleString;
            }
        }

        /// <summary>
        /// Коллекция элементов типа EventListItem
        /// </summary>
        public ObservableCollection<EventListItem> EventList
        {
            get
            {
                return _ELModel.EventList;
            }
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
                      _ELModel.ChangeDay(1);
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
                      _ELModel.ChangeDay(-1);
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

        /// <summary>
        /// Ловим изменения Model и оповещаем View
        /// </summary>
        public void ModelpropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
