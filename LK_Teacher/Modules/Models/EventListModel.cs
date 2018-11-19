using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Models
{
    class EventListModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Инициализируем объект с указанным днем
        /// </summary>
        public EventListModel()
        {
            EventListUpdater.Instance.Subscribe(Update);

            CountClasses = UtilFunctions.TimeofEvents.Length;

            EventList = new ObservableCollection<EventListItem>();

        }

        //Константы
        private readonly int CountClasses;

        //Поля
        private DateTime _DayOfEvent;

        //Свойства

        /// <summary>
        /// Коллекция элементов типа EventListItem
        /// </summary>
        private ObservableCollection<EventListItem> _EventList;
        public ObservableCollection<EventListItem> EventList
        {
            get
            {
                return _EventList;
            }
            set
            {
                _EventList = value;
            }
        }

        /// <summary>
        /// Строка с информация о дне события
        /// </summary>
        private string _TitleString;
        public string TitleString
        {
            get { return _TitleString; }
            set
            {
                _TitleString = value;
                OnPropertyChanged("TitleString");
            }
        }

        /// <summary>
        /// Проверка на рабочий день
        /// </summary>
        public bool IsWorkDay
        {
            get
            {
                if (_DayOfEvent.DayOfWeek != DayOfWeek.Sunday && _DayOfEvent.DayOfWeek != DayOfWeek.Saturday) return true;
                else return false;
            }
        }

        /// <summary>
        /// Инициализация списка по указанному дню
        /// </summary>
        public void InitializeEventList(DateTime day)
        {
            //Чистим панель
            EventList.Clear();

            //Сохраним день
            _DayOfEvent = day;

            //Заполняем список
            if (IsWorkDay)
            {
                TitleString = $"РАСПИСАНИЕ НА {day.ToString("dd/MM/yyyy")} - {UtilFunctions.DayOfWeek(day).ToUpper()}";

                for (int number_class = 0; number_class < CountClasses; number_class++)
                {
                    var eli = new EventListItem(day, number_class);
                    EventList.Add(eli);
                }
            }
            else TitleString = "ЗАПИСЬ ЗАКРЫТА - Сегодня нерабочий день!";
        }

        /// <summary>
        /// Изменить DayOfEvent на указанное число дней.
        /// </summary>
        /// <param name="offset">Количество добавляемых дней (+1 / -1)</param>
        public void ChangeDay(int offset)
        {
            InitializeEventList(_DayOfEvent.AddDays(offset));
        }

        private void Update()
        {
            InitializeEventList(_DayOfEvent);
        }

        //Ловим изменения
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
