using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            set
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

        public EventFormModel()
        {
            EventFormDataHandler.Instance.Subscribe(ShowInfo);
        }

        private void ShowInfo(int id_event)
        {
            IsSetEvent = true;

            _IdEvent = id_event;

            Hashtable dataEvent = DBApi.GetDataEvent(id_event);
            //Visibility = Visibility.Visible;
            //labDayWeek.Content = eventGridItem.GetNameDay();
            //var date = dataEvent["date_event"].ToString();
            //labDate.Content = eventGridItem.DayOfEvent.ToString("dd/MM/yyyy HH:mm");
            TitleEvent = dataEvent["title_event"].ToString();
            //labTypeEvent.Content = eventGridItem.GetTypeEvent();
            //tblDdescription.Text = dataEvent["description_event"].ToString();


        }
    }
}
