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

        /// <summary>
        /// Инициализируем VM
        /// </summary>
        /// <param name="day">День события</param>
        public EventListViewModel(DateTime day)
        {
            EventForm = new EventForm();
            _ELModel = new EventListModel(day);
            _ELModel.PropertyChanged += ModelpropertyChanged;
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

        /// <summary>
        /// Ловим изменения Model и оповещаем View
        /// </summary>
        public void ModelpropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
