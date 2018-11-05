using LK_Teacher.Assets;
using LK_Teacher.Moduls.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LK_Teacher.Moduls
{
    /// <summary>
    /// Логика взаимодействия для EventGridItem2.xaml
    /// </summary>
    public partial class EventGridItem : UserControl, IEventItem
    {
        //Константы ----------------------------------------------

        const int NONE_TYPE = 0;
        const int CLASS_TYPE = 1;
        const int CONFERENCE_TYPE = 2;
        const int EVENT_TYPE = 3;

        //Ссылки ------------------------------------------------

        //Ссылка на родительское окно
        public MainWindow ParentWindow { get; private set; }

        //Свойства -----------------------------------

        //Id события
        public int IdEvent { get; set; } = -1;

        //Тип события
        public int TypeOfEvent { get; set; }

        //Статус события
        public bool StatusEvent { get; set; } = false;

        //День события
        public DateTime DayOfEvent { get; set; }

        //Номер строки при списке
        public int Row { get; set; }

        //Относительное позиционирование начиная от (1,1) для сетки
        public int RelativeRow { get; set; }
        public int RelativeCol { get; set; }

        //Абсолютное позиционирование начиная с (0,0) для сетки
        public int AbsoluteRow { get; set; }
        public int AbsoluteCol { get; set; }

        //Номер (пары, мероприятия или события) - счет идет от 1
        public int NumberClass { get { return AbsoluteRow; } }

        //Установлено-ли событие
        public bool IsSetEvent
        {
            get
            {
                if (IdEvent != -1) return true;
                else return false;
            }
        }

        private string NameStyleClass = null;

        Button IEventItem.btAction => btAction;

        TextBlock IEventItem.tblTitle => tblTitle;

        public EventGridItem(Window parent,DateTime mondayThisWeek, int relativeCol, int relativeRow)
        {
            InitializeComponent();
            this.RelativeCol = relativeCol;
            this.RelativeRow = relativeRow;

            this.AbsoluteCol = relativeCol + 1;
            this.AbsoluteRow = relativeRow + 1;

            ParentWindow = (MainWindow)parent;

            DayOfEvent = mondayThisWeek.AddDays(relativeCol);
            DayOfEvent = DayOfEvent.Add(UtilityApi.TimeOfClasses[NumberClass - 1]);

            this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Assets/Styles/StyleEventGrid.xaml") };

            ParentWindow.TurnOnOff += TurnOnOffHandler;

            Initialize();
        }

        private void TurnOnOffHandler(DateTime dayEvent)
        {
            if (dayEvent == DayOfEvent)
            {
                if (DayOfEvent.Add(UtilityApi.DurationClass) >= DateTime.Now)
                {
                    NameStyleClass = "Current";
                    Style st = (Style)this.TryFindResource(NameStyleClass);
                    btAction.Style = st;
                }
                else
                {
                    NameStyleClass = "Disable";
                    Style st = (Style)this.TryFindResource(NameStyleClass);
                    btAction.Style = st;
                }
            }
        }

        public void Initialize()
        {
            btAction.Tag = this;

            if (DataBaseApi.IsConnection)
            {
                Hashtable ht = DataBaseApi.GetEventWhereDate(DayOfEvent);
                if (ht.Count != 0)
                {
                    IdEvent = Convert.ToInt32(ht["id_event"].ToString());
                    StatusEvent = Convert.ToBoolean(ht["status_event"]);

                    if (
                        DayOfEvent.Add(new TimeSpan(1,30,0)) < DateTime.Now 
                        &&
                        StatusEvent
                        )
                    {
                        StatusEvent = false;
                        DataBaseApi.UpdateStatusEvent(IdEvent, StatusEvent);
                    }

                    TypeOfEvent = Convert.ToInt32(ht["type_event"]);

                    btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)(PlusButtonClick));
                    btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(EventButtonClick));

                    tblTitle.Text = ht["title_event"].ToString(); ;

                    switch (TypeOfEvent)
                    {
                        case 1:
                            NameStyleClass = "ClassButton";
                            break;
                        case 2:
                            NameStyleClass = "СonferenceButton";
                            break;
                        case 3:
                            NameStyleClass = "EventButton";
                            break;
                    }

                    if (StatusEvent)
                    {
                        labTag.Style = (Style)this.TryFindResource("NoComplete");
                    }
                    else
                    {
                        labTag.Style = (Style)this.TryFindResource("Complete");
                    }
                    btAction.Style = (Style)this.TryFindResource(NameStyleClass);
                }
                else
                {
                    if (DayOfEvent > DateTime.Now)
                    {
                        NameStyleClass = "PlusButton";
                        btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(PlusButtonClick));
                        btAction.Style = (Style)this.TryFindResource(NameStyleClass);
                        tblTitle.Text = "Добавить событие?";
                        labTag.Style = (Style)this.TryFindResource("NoComplete");
                    }
                    else
                    {
                        NameStyleClass = "Disable";
                        tblTitle.Text = "Пусто";
                        labTag.Style = (Style)this.TryFindResource("DisComplete");
                        btAction.Style = (Style)this.TryFindResource(NameStyleClass);
                    }
                }
            }
        }

        public void PlusButtonClick(object sender, RoutedEventArgs e)
        {

            AddEvent formAddEvent = new AddEvent(((Button)sender).Tag);
            formAddEvent.ShowDialog();
        }

        public void EventButtonClick(object sender, RoutedEventArgs e)
        {
            ParentWindow.eventForm.InitializeData(this);
        }

        public string GetTypeEvent()
        {
            switch (TypeOfEvent)
            {
                case 1:
                    return "Занятие";
                case 2:
                    return "Совещание";
                case 3:
                    return "Мероприятие";
            }
            return null;
        }

        public string GetNameDay()
        {
            switch ((int)DayOfEvent.DayOfWeek)
            {
                case 1:
                    return "Понедельник";
                case 2:
                    return "Вторник";
                case 3:
                    return "Среда";
                case 4:
                    return "Четверг";
                case 5:
                    return "Пятница";
            }
            return null;
        }
    }
}
