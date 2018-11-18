using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using LK_Teacher.Moduls;
using System.Collections;

namespace LK_Teacher.Assets
{
    class EventGridItem3 : Grid
    {
        public static TimeSpan[] timeClasses = { new TimeSpan(8, 30, 0), new TimeSpan(10, 10, 0), new TimeSpan(12, 10, 0), new TimeSpan(13, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(17, 30, 0) };

        //Константы ----------------------------------------------

        const int NONE_TYPE = 0;
        const int CLASS_TYPE = 1;
        const int CONFERENCE_TYPE = 2;
        const int EVENT_TYPE = 3;

        //Ссылки ------------------------------------------------

        //Ссылка на родительское окно
        public MainWindow ParentWindow { get; private set; }
        //Ссылка на кнопку события
        public Button Button { get; private set; }

        //Свойства -----------------------------------

        //Id события
        public int IdEvent { get; set; } = -1;

        //Тип события
        public int TypeOfEvent { get; set; }

        //День события
        public DateTime DayOfEvent { get; private set; }

        //Относительное позиционирование начиная от (1,1)
        public int RelativeRow { get; set; }
        public int RelativeCol { get; set; }
        
        //Абсолютное позиционирование начиная с (0,0)
        public int AbsoluteRow { get; set; }
        public int AbsoluteCol { get; set; }
        
        //Номер (пары, мероприятия или события) - счет идет от 1
        public int NumberClass { get { return AbsoluteRow; } }

        //Установлено-ли событие
        public bool IsSetEvent {
            get {
                if (IdEvent != -1) return true;
                else return false;
            }
        }

        //Возвращает дату (DateTime)  понедельника недели события 
        static DateTime GetMondayOfThisWeek()
        {
            var date = DateTime.Today;
            while (date.DayOfWeek != System.DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        
        public EventGridItem3(Window parent, int relativeCol, int relativeRow)
        {
            this.RelativeCol = relativeCol;
            this.RelativeRow = relativeRow;

            this.AbsoluteCol = relativeCol+1;
            this.AbsoluteRow = relativeRow+1;

            ParentWindow = (MainWindow)parent;

            DayOfEvent = EventGridItem3.GetMondayOfThisWeek().AddDays(relativeCol);
            DayOfEvent = DayOfEvent.Add(timeClasses[NumberClass-1]);

            this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Assets/Styles.xaml") };

            Initialize();
        }

        private void Initialize()
        {
            Button = new Button();
            Button.Tag = this;

            if (Api.IsConnection)
            {
                Hashtable ht = Api.CheckDateEvent(DayOfEvent);
                if (ht.Count != 0)
                {
                    IdEvent = Convert.ToInt32(ht["id_event"].ToString());
                    TypeOfEvent = Convert.ToInt32(ht["type_event"]);
                    Button.AddHandler(Button.ClickEvent, new RoutedEventHandler(EventButtonClick));
                    string nameStyleClass = "";
                    switch (TypeOfEvent) {
                        case 1:
                            nameStyleClass = "ClassButton";
                            break;
                        case 2:
                            nameStyleClass = "СonferenceButton";
                            break;
                        case 3:
                            nameStyleClass = "EventButton";
                            break;
                    }

                    Button.Style = (Style)this.TryFindResource(nameStyleClass);
                }
                else
                {
                    Button.AddHandler(Button.ClickEvent, new RoutedEventHandler(PlusButtonClick));
                    Button.Style = (Style)this.TryFindResource("PlusButton");
                }
                this.Children.Add(Button);
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
