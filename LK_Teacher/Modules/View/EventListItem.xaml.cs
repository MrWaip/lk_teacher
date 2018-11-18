using LK_Teacher.Assets;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using LK_Teacher.Modules.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LK_Teacher.Modules.View
{

    public partial class EventListItem : UserControl
    {
        ////Константы ----------------------------------------------

        //const int NONE_TYPE = 0;
        //const int CLASS_TYPE = 1;
        //const int CONFERENCE_TYPE = 2;
        //const int EVENT_TYPE = 3;

        ////Ссылки ------------------------------------------------

        ////Ссылка на родительское окно
        //public ShellView ParentWindow { get; private set; }

        //public EventForm EventForm { get; private set; }

        ////Свойства -----------------------------------

        ////Id события
        //public int IdEvent { get; set; } = -1;

        ////Тип события
        //public int TypeOfEvent { get; set; }

        ////Статус события
        //public bool StatusEvent { get; set; } = false;

        ////День события
        //public DateTime DayOfEvent { get; set; }

        ////Номер строки при списке
        //public int Row { get; set; }

        ////Относительное позиционирование начиная от (1,1) для сетки
        //public int RelativeRow { get; set; }
        //public int RelativeCol { get; set; }

        ////Абсолютное позиционирование начиная с (0,0) для сетки
        //public int AbsoluteRow { get; set; }
        //public int AbsoluteCol { get; set; }

        ////Номер (пары, мероприятия или события) - счет идет от 1
        //public int NumberClass { get { return AbsoluteRow; } }

        ////Установлено-ли событие
        //public bool IsSetEvent
        //{
        //    get
        //    {
        //        if (IdEvent != -1) return true;
        //        else return false;
        //    }
        //}

        //Button IEventItem.btAction => btAction;

        //TextBlock IEventItem.tblTitle => tblTitle;


        /// <summary>
        /// Инициализируем элемент коллекции EventList
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="evf"></param>
        /// <param name="dayEvent">День события</param>
        /// <param name="number_class">Номмер события. Смотри UtilFunctions.</param>
        public EventListItem(Window parent, EventForm evf, DateTime dayEvent, int number_class)
        {
            InitializeComponent();

            DataContext = new EventListItemViewModel(dayEvent, number_class);

            //this.Row = number_class;

            //ParentWindow = (ShellView)parent;

            //EventForm = evf;

            //DayOfEvent = dayEvent;
            //DayOfEvent = DayOfEvent.Add(UtilFunctions.TimeOfClasses[Row]);

            ////ParentWindow.TurnOnOff += TurnOnOffHandler;

            //this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Assets/Styles/StyleEventList.xaml") };

            //Initialize();
        }

        //private void TurnOnOffHandler(DateTime dayEvent)
        //{
        //    if (dayEvent == DayOfEvent)
        //    {
        //        if (DayOfEvent.Add(UtilFunctions.DurationClass) >= DateTime.Now)
        //        {
        //            Style st = (Style)this.TryFindResource("ActiveMode");
        //            labTag.Style = (Style)this.TryFindResource("EventNow");
        //            bgTitle.Style = st;
        //        }
        //        else
        //        {
        //            Style st = (Style)this.TryFindResource("NoMode");
                    
        //            if (TypeOfEvent == -1)
        //            {
        //                labTag.Style = (Style)this.TryFindResource("DisComplete");
        //            }
        //            else
        //            {
        //                labTag.Style = (Style)this.TryFindResource("Complete");
        //            }
        //            bgTitle.Style = st;
        //        }
        //    }
        //}

        //public void Initialize()
        //{
        //    btAction.Tag = this;
        //    lbDate.Content = DayOfEvent.ToString("HH:mm") + " - " + DayOfEvent.Add(new TimeSpan(1, 30, 0)).ToString("HH:mm");

        //    bgTitle.Style = (Style)this.TryFindResource("NoMode");

        //    if (DataBaseApi.IsConnection)
        //    {
        //        Hashtable ht = DataBaseApi.GetEventWhereDate(DayOfEvent);
        //        if (ht.Count != 0)
        //        {
        //            IdEvent = Convert.ToInt32(ht["id_event"].ToString());
        //            StatusEvent = Convert.ToBoolean(ht["status_event"]);

        //            if (
        //                DayOfEvent.Add(new TimeSpan(1,30,0)) < DateTime.Now 
        //                &&
        //                StatusEvent
        //                )
        //            {
        //                StatusEvent = false;
        //                DataBaseApi.UpdateStatusEvent(IdEvent, StatusEvent);
        //            }

        //            TypeOfEvent = Convert.ToInt32(ht["type_event"]);

        //            btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)(PlusButtonClick));
        //            btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(EventButtonClick));
        //            tblTitle.Text = ht["title_event"].ToString();
        //            string nameStyleClass = "";
        //            switch (TypeOfEvent)
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

        //            if (StatusEvent)
        //            {
        //                labTag.Style = (Style)this.TryFindResource("NoComplete"); 
        //            }
        //            else
        //            {
        //                labTag.Style = (Style)this.TryFindResource("Complete");
        //            }
        //            btAction.Style = (Style)this.TryFindResource(nameStyleClass);
        //        }
        //        else
        //        {
        //            if (DayOfEvent > DateTime.Now)
        //            {
        //                btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(PlusButtonClick));
        //                btAction.Style = (Style)this.TryFindResource("PlusButton");
        //                labTag.Style = (Style)this.TryFindResource("NoComplete");
        //            }
        //            else
        //            {
        //                labTag.Style = (Style)this.TryFindResource("DisComplete");
        //                btAction.Style = (Style)this.TryFindResource("Disable");
        //            }
        //        }
        //    }
        //}

        //public void PlusButtonClick(object sender, RoutedEventArgs e)
        //{

        //    AddEvent formAddEvent = new AddEvent(((Button)sender).Tag);
        //    formAddEvent.ShowDialog();
        //}

        //public void EventButtonClick(object sender, RoutedEventArgs e)
        //{
        //    EventForm.InitializeData(this);
        //}

        //public string GetTypeEvent()
        //{
        //    switch (TypeOfEvent)
        //    {
        //        case 1:
        //            return "Занятие";
        //        case 2:
        //            return "Совещание";
        //        case 3:
        //            return "Мероприятие";
        //    }
        //    return null;
        //}

        //public string GetNameDay()
        //{
        //    switch ((int)DayOfEvent.DayOfWeek)
        //    {
        //        case 1:
        //            return "Понедельник";
        //        case 2:
        //            return "Вторник";
        //        case 3:
        //            return "Среда";
        //        case 4:
        //            return "Четверг";
        //        case 5:
        //            return "Пятница";
        //    }
        //    return null;
        //}
    }
}
