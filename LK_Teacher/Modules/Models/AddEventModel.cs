using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Models
{
    class AddEventModel: BindableBase
    {
        private string _TitleEvent = "";
        public string TitleEvent
        {
            get { return _TitleEvent; }
            set
            {
                _TitleEvent = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        private DateTime _DateEvent;
        public DateTime DateEvent
        {
            get { return _DateEvent; }
            set
            {
                _DateEvent = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        private string _DayOfWeekEvent ="";
        public string DayOfWeekEvent
        {
            get { return _DayOfWeekEvent; }
            set
            {
                _DayOfWeekEvent = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        private int _TypeOfEvent = 0;
        public int TypeOfEvent
        {
            get { return _TypeOfEvent; }
            set
            {
                _TypeOfEvent = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        private string _DescriptionEvent = "";
        public string DescriptionEvent
        {
            get { return _DescriptionEvent; }
            set
            {
                _DescriptionEvent = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        public AddEventModel(DateTime date_event)
        {
            DateEvent = date_event;
            DayOfWeekEvent = UtilFunctions.DayOfWeek(DateEvent);
        }

        public void AddEvent()
        {
            int id = DBApi.AddNewEvent(TitleEvent, DateEvent, DescriptionEvent, TypeOfEvent);
            if (id != -1)
            {
                EventContainerUpdater.Instance.Publish();
                EventFormDataHandler.Instance.Publish(id);
            }
        }
    }
}
