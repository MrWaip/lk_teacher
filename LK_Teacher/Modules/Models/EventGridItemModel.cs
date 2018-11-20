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

namespace LK_Teacher.Modules.Models
{
    class EventGridItemModel : BindableBase
    {
        const int NONE_TYPE = 0;
        const int CLASS_TYPE = 1;
        const int CONFERENCE_TYPE = 2;
        const int EVENT_TYPE = 3;

        private int _IdEvent = -1;

        public enum StatusViewEvent
        {
            FINISHED_NOT_INSTALLED, FINISHED_INSTALLED, NOT_FINISHED_NOT_INSTALLED, NOT_FINISHED_INSTALLED
        }

        private int _TypeOfEvent = NONE_TYPE;
        public int TypeOfEvent
        {
            get { return _TypeOfEvent; }
            private set
            {
                _TypeOfEvent = value;
                RaisePropertyChanged("TypeOfEvent");
            }
        }

        private bool _StatusEvent = false;
        public bool StatusEvent
        {
            get { return _StatusEvent; }
            private set
            {
                _StatusEvent = value;
                RaisePropertyChanged("StatusEvent");
            }
        }

        private DateTime _DateOfEvent;
        public DateTime DateOfEvent
        {
            get { return _DateOfEvent; }
            set { SetProperty(ref _DateOfEvent, value); }
        }


        private int _AbsoluteCol;
        public int AbsoluteCol
        {
            get { return _AbsoluteCol; }
            set { SetProperty(ref _AbsoluteCol, value); }
        }

        private int _AbsoluteRow;
        public int AbsoluteRow
        {
            get { return _AbsoluteRow; }
            set { SetProperty(ref _AbsoluteRow, value); }
        }

        private string _TitleEvent = "Пусто";
        public string TitleEvent
        {
            get { return _TitleEvent; }
            set { SetProperty(ref _TitleEvent, value); }
        }

        //Имя стиля для кнопки основной кнопки
        private string _ActionButtonStyle;
        public string ActionButtonStyle
        {
            get { return _ActionButtonStyle; }
            private set
            {
                _ActionButtonStyle = value;
                RaisePropertyChanged("ActionButtonStyle");
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
                RaisePropertyChanged("LabelStatusStyle");
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

        public EventGridItemModel(DateTime monday_this_week, int col, int row)
        {
            AbsoluteCol = col + 1;
            AbsoluteRow = row + 1;

            //monday_this_week - понедельник этой недели, а нам нужно еще и время, поэтому добавляем времяя и день, в зависимости от дня недели
            DateOfEvent = monday_this_week.AddDays(col);
            DateOfEvent = DateOfEvent.Add(UtilFunctions.TimesOfEvents[row]);

            Initialize();
        }

        public void Initialize()
        {
            //btAction.Tag = this;
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

                //btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)(PlusButtonClick));
                //btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(EventButtonClick));

                TitleEvent = ht["title_event"].ToString(); ;

                switch (TypeOfEvent)
                {
                    case 1:
                        ActionButtonStyle = "ClassButtonGrid";
                        break;
                    case 2:
                        ActionButtonStyle = "СonferenceButtonGrid";
                        break;
                    case 3:
                        ActionButtonStyle = "EventButtonGrid";
                        break;
                }

                if (StatusEvent)
                {
                    LabelStatusStyle = "NoCompleteGrid";
                }
                else
                {
                    LabelStatusStyle = "CompleteGrid";
                }
            }
            else
            {
                if (DateOfEvent > DateTime.Now)
                {
                    ActionButtonStyle = "AddButtonGrid";
                    //btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(PlusButtonClick));
                    //btAction.Style = (Style)this.TryFindResource(NameStyleClass);
                    TitleEvent = "Добавить событие?";
                    LabelStatusStyle = "NoCompleteGrid";
                }
                else
                {
                    ActionButtonStyle = "DisableGrid";
                    TitleEvent = "Пусто";
                    LabelStatusStyle = "DisCompleteGrid";
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
    }
}
