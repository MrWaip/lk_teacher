using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Models
{
    class EventListItemModel : INotifyPropertyChanged
    {
        ////Константы ----------------------------------------------

        const int NONE_TYPE = 0;
        const int CLASS_TYPE = 1;
        const int CONFERENCE_TYPE = 2;
        const int EVENT_TYPE = 3;

        ////Ссылки ------------------------------------------------

        ////Ссылка на родительское окно
        //public ShellView ParentWindow { get; private set; }

        //public EventForm EventForm { get; private set; }

        ////Свойства -----------------------------------

        //Id события
        private int IdEvent = -1;

        //Тип события
        private int _TypeOfEvent = NONE_TYPE;
        public int TypeOfEvent
        {
            get { return _TypeOfEvent; }
            private set
            {
                _TypeOfEvent = value;
                OnPropertyChanged("TypeOfEvent");
            }
        }

        //Статус события (Активно = true | завершено = false)
        private bool _StatusEvent = false;
        public bool StatusEvent
        {
            get { return _StatusEvent; }
            private set
            {
                _StatusEvent = value;
                OnPropertyChanged("StatusEvent");
            }
        }

        //День события
        private DateTime _DayOfEvent;
        public DateTime DayOfEvent
        {
            get { return _DayOfEvent; }
            private set
            {
                _DayOfEvent = value;
                OnPropertyChanged("DayOfEvent");
            }
        }

        //Номер события \ номер элемента в списке
        private int _NumberOfEvent;
        public int NumberOfEvent
        {
            get { return _NumberOfEvent; }
            private set
            {
                _NumberOfEvent = value;
                OnPropertyChanged("NumberOfEvent");
            }
        }

        //Мета информаци элемента о дате в виде строки
        private string _DateMetaInfo;
        public string DateMetaInfo
        {
            get { return _DateMetaInfo; }
            private set
            {
                _DateMetaInfo = value;
                OnPropertyChanged("DateMetaInfo");
            }
        }

        //Мета информация заголовка события
        private string _TitleMetaInfo;
        public string TitleMetaInfo
        {
            get { return _TitleMetaInfo; }
            private set
            {
                _TitleMetaInfo = value;
                OnPropertyChanged("TitleMetaInfo");
            }
        }
        
        //Имя стиля для кнопки основной кнопки
        private string _ActionButtonStyle;
        public string ActionButtonStyle
        {
            get { return _ActionButtonStyle; }
            private set
            {
                _ActionButtonStyle = value;
                OnPropertyChanged("ActionButtonStyle");
            }
        }

        //Стили метки о статусе события
        private string _LabelStatusStyle;
        public string LabelStatusStyle
        {
            get { return _LabelStatusStyle; }
            private set
            {
                _LabelStatusStyle = value;
                OnPropertyChanged("LabelStatusStyle");
            }
        }

        //Стиль заголовка события
        private string _TitleStyle;
        public string TitleStyle
        {
            get { return _TitleStyle; }
            private set
            {
                _TitleStyle = value;
                OnPropertyChanged("TitleStyle");
            }
        }

        //Установлено ли сообытие
        public bool IsEventSet
        {
            get
            {
                if (TypeOfEvent == 0)
                    return false;
                else return true;
            }
        }

        //Button IEventItem.btAction => btAction;

        //TextBlock IEventItem.tblTitle => tblTitle;

        public EventListItemModel(DateTime day_event, int number_event)
        {
            NumberOfEvent = number_event;

            //ParentWindow = (ShellView)parent;

            //EventForm = evf;

            DayOfEvent = day_event;
            DayOfEvent = DayOfEvent.Add(UtilFunctions.TimeofEvents[number_event]);

            ////ParentWindow.TurnOnOff += TurnOnOffHandler;

            Initialize();
        }

        public void Initialize()
        {
            //btAction.Tag = this;
            DateMetaInfo = DayOfEvent.ToString("HH:mm") + " - " + DayOfEvent.Add(new TimeSpan(1, 30, 0)).ToString("HH:mm");

            TitleStyle = "NoMode";

            if (DBApi.IsConnection)
            {
                Hashtable ht = DBApi.GetEventWhereDate(DayOfEvent);
                if (ht.Count != 0)
                {
                    IdEvent = Convert.ToInt32(ht["id_event"].ToString());
                    StatusEvent = Convert.ToBoolean(ht["status_event"]);

                    if (
                        DayOfEvent.Add(new TimeSpan(1, 30, 0)) < DateTime.Now
                        &&
                        StatusEvent
                        )
                    {
                        StatusEvent = false;
                        DBApi.UpdateStatusEvent(IdEvent, StatusEvent);
                    }

                    TypeOfEvent = Convert.ToInt32(ht["type_event"]);

                    //btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)(PlusButtonClick));
                    //btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(EventButtonClick));

                    TitleMetaInfo = ht["title_event"].ToString();

                    switch (TypeOfEvent)
                    {
                        case 1:
                            ActionButtonStyle = "ClassButton";
                            break;
                        case 2:
                            ActionButtonStyle = "СonferenceButton";
                            break;
                        case 3:
                            ActionButtonStyle = "EventButton";
                            break;
                    }

                    if (StatusEvent)
                    {
                        LabelStatusStyle = "NoComplete";
                    }
                    else
                    {
                        LabelStatusStyle = "Complete";
                    }
                }
                else
                {
                    if (DayOfEvent > DateTime.Now)
                    {
                        //btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(PlusButtonClick));
                        ActionButtonStyle = "PlusButton";
                        LabelStatusStyle = "NoComplete";
                    }
                    else
                    {
                        LabelStatusStyle = "DisComplete";
                        ActionButtonStyle = "Disable";
                    }
                }
            }
        }

        public void ShowInfoEvent()
        {
            EventFormDataHandler.Instance.Publish(IdEvent);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
