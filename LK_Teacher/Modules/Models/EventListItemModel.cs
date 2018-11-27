using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using Prism.Events;
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

        public enum StatusViewEvent
        {
            FINISHED_NOT_INSTALLED, FINISHED_INSTALLED, NOT_FINISHED_NOT_INSTALLED, NOT_FINISHED_INSTALLED
        }

        //Свойства -----------------------------------

        //Id события
        private int _IdEvent = -1;
        private SubscriptionToken _SubToken;

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
        private DateTime _DateOfEvent;
        public DateTime DateOfEvent
        {
            get { return _DateOfEvent; }
            private set
            {
                _DateOfEvent = value;
                OnPropertyChanged("DateOfEvent");
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
        private string _TitleMetaInfo = "Пусто";
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
        public StatusViewEvent GetStatusViewEvent
        {
            get
            {
                if (DateOfEvent < DateTime.Now)
                {
                    if (_IdEvent == -1)
                    {
                        return StatusViewEvent.FINISHED_NOT_INSTALLED;
                    }
                    else
                    {
                        return StatusViewEvent.FINISHED_INSTALLED;
                    }
                }
                else
                {
                    if (_IdEvent == -1)
                    {
                        return StatusViewEvent.NOT_FINISHED_NOT_INSTALLED;
                    }
                    else
                    {
                        return StatusViewEvent.NOT_FINISHED_INSTALLED;
                    }
                }

            }
        }

        public EventListItemModel(DateTime day_event, int number_event)
        {
            DateOfEvent = day_event.Add(UtilFunctions.TimesOfEvents[number_event]);
            _SubToken = EventTimer.Instance.Subscribe(CheckCurrentEvent);
            Initialize();
        }

        public void Initialize()
        {
            DateMetaInfo = DateOfEvent.ToString("HH:mm") + " - " + DateOfEvent.Add(new TimeSpan(1, 30, 0)).ToString("HH:mm");

            TitleStyle = "NoMode";

            if (DBApi.IsConnection)
            {
                Hashtable ht = DBApi.GetEventWhereDate(DateOfEvent);
                if (ht.Count != 0)
                {
                    _IdEvent = Convert.ToInt32(ht["id_event"].ToString());
                    StatusEvent = Convert.ToBoolean(ht["status_event"]);

                    if (
                        DateOfEvent.Add(new TimeSpan(1, 30, 0)) < DateTime.Now
                        &&
                        StatusEvent
                        )
                    {
                        StatusEvent = false;
                        DBApi.UpdateStatusEvent(_IdEvent, StatusEvent);
                    }

                    TypeOfEvent = Convert.ToInt32(ht["type_event"]);

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
                    TitleMetaInfo = "Пусто";
                    TypeOfEvent = NONE_TYPE;

                    if (DateOfEvent > DateTime.Now)
                    {
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
            EventFormDataHandler.Instance.Publish(_IdEvent);
        }

        public void AddEvent()
        {
            new AddEventView(DateOfEvent).ShowDialog();
        }

        private void CheckCurrentEvent(EventTimerArgs arg)
        {
            if (DateOfEvent == DateTime.Today.Add(arg.CurrentTimeEvent))
            {
                if (arg.TimeToEnd.TotalSeconds <= 0)
                {
                    if (TypeOfEvent == NONE_TYPE)
                    {
                        LabelStatusStyle = "DisComplete";
                    }
                    else
                    {
                        LabelStatusStyle = "Complete";
                    }
                    TitleStyle = "NoMode";
                }
                else
                {
                    LabelStatusStyle = "EventNow";
                    TitleStyle = "ActiveMode";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        ~EventListItemModel()
        {
            EventTimer.Instance.Unsubscribe(_SubToken);
        }
    }
}
