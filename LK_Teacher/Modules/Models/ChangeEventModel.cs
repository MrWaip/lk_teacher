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
    class ChangeEventModel : BindableBase
    {
        private int _IdEvent;

        private string _TitleEvent;
        public string TitleEvent
        {
            get { return _TitleEvent; }
            set
            {
                _TitleEvent = value;
                RaisePropertyChanged("CanSave");
            }
        }

        private DateTime _DateEvent;
        public DateTime DateEvent
        {
            get { return _DateEvent; }
            set
            {
                _DateEvent = value;
                RaisePropertyChanged("CanSave");
            }
        }

        private string _DayOfWeekEvent;
        public string DayOfWeekEvent
        {
            get { return _DayOfWeekEvent; }
            set
            {
                _DayOfWeekEvent = value;
                RaisePropertyChanged("CanSave");
            }
        }

        private int _TypeOfEvent;
        public int TypeOfEvent
        {
            get { return _TypeOfEvent; }
            set
            {
                _TypeOfEvent = value;
                RaisePropertyChanged("CanSave");
            }
        }

        private string _DescriptionEvent;
        public string DescriptionEvent
        {
            get { return _DescriptionEvent; }
            set
            {
                _DescriptionEvent = value;
                RaisePropertyChanged("CanSave");
            }
        }

        public ChangeEventModel(int id_event)
        {
            _IdEvent = id_event;

            Hashtable dataEvent = DBApi.GetDataEvent(id_event);

            DateEvent = DateTime.Parse(dataEvent["date_event"] as string);

            DayOfWeekEvent = UtilFunctions.DayOfWeek(DateEvent);

            TitleEvent = dataEvent["title_event"].ToString();

            TypeOfEvent = Convert.ToInt32(dataEvent["type_event"]);

            DescriptionEvent = dataEvent["description_event"].ToString();
        }

        public void SaveChanges()
        {

            DBApi.UpdateEvent(_IdEvent, TitleEvent, DateEvent, DescriptionEvent, TypeOfEvent);
            EventFormDataHandler.Instance.Publish(_IdEvent);
            EventListUpdater.Instance.Publish();
            //            eventGridItem.TypeOfEvent = cbTypeEvent.SelectedIndex;
            //            string nameStyleClass = "";
            //            switch (cbTypeEvent.SelectedIndex)
            //            {
            //                case 1:
            //                    nameStyleClass = "ClassButton";
            //                    break;
            //                case 2:
            //                    nameStyleClass = "СonferenceButton";
            //                    break;
            //                case 3:
            //                    nameStyleClass = "EventButton";
            //                    break;
            //            }

            //            eventGridItem.btAction.Style = (Style)eventGridItem.TryFindResource(nameStyleClass);
            //            eventGridItem.btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(eventGridItem.EventButtonClick));
            //            eventGridItem.tblTitle.Text = tbTitle.Text;
            //            this.Close();
        }
    }
}
