using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LK_Teacher.Modules.Models
{
    class EventFormModel : BindableBase
    {
        private int _IdEvent = 0;

        /// <summary>
        /// Установлено ли событие в EventForm.
        /// </summary>
        private bool _IsSetEvent;
        public bool IsSetEvent
        {
            get { return _IsSetEvent; }
            private set
            {
                _IsSetEvent = value;
                RaisePropertyChanged("Visibility");
            }
        }

        private string _TitleEvent;
        public string TitleEvent
        {
            get { return _TitleEvent; }
            private set
            {
                _TitleEvent = value;
                RaisePropertyChanged("TitleEvent");
            }
        }

        private DateTime _DateEvent;
        public DateTime DateEvent
        {
            get { return _DateEvent; }
            private set { SetProperty(ref _DateEvent, value); }
        }

        private string _DayOfWeekEvent;
        public string DayOfWeekEvent
        {
            get { return _DayOfWeekEvent; }
            private set
            {
                _DayOfWeekEvent = value;
                RaisePropertyChanged("DayOfWeekEvent");
            }
        }

        private int _TypeOfEvent;
        public int TypeOfEvent
        {
            get { return _TypeOfEvent; }
            private set { SetProperty(ref _TypeOfEvent, value); }
        }

        private string _DescriptionEvent;
        public string DescriptionEvent
        {
            get { return _DescriptionEvent; }
            private set { SetProperty(ref _DescriptionEvent, value); }
        }

        public EventFormModel()
        {
            EventFormDataHandler.Instance.Subscribe(ShowInfo);
        }

        private void ShowInfo(int id_event)
        {
            IsSetEvent = true;

            _IdEvent = id_event;

            Hashtable dataEvent = DBApi.GetDataEvent(id_event);

            DateEvent = DateTime.Parse(dataEvent["date_event"] as string);

            DayOfWeekEvent = UtilFunctions.DayOfWeek(DateEvent);

            TitleEvent = dataEvent["title_event"].ToString();

            TypeOfEvent = Convert.ToInt32(dataEvent["type_event"]);

            DescriptionEvent = dataEvent["description_event"].ToString();

        }

        public void ChangeEvent()
        {
            new ChangeEvent(_IdEvent).ShowDialog();
        }

        public void DeleteEvent()
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить событие?", "Удаление события", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                DBApi.DeleteEvent(_IdEvent);
                IsSetEvent = false;
                EventContainerUpdater.Instance.Publish();
            }

        }
    }
}
