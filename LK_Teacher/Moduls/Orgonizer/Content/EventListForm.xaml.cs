using LK_Teacher.Moduls.API;
using System;
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

namespace LK_Teacher.Moduls.Content
{
    /// <summary>
    /// Логика взаимодействия для TodayForm.xaml
    /// </summary>
    public partial class EventListForm : UserControl, IEventForm
    {
        //Константы
        readonly int CountClasses;

        public DateTime Day { get; private set; }

        private MainWindow ParentWindow;

        public EventListForm(MainWindow mainWindow, DateTime day)
        {
            CountClasses = UtilityApi.TimeOfClasses.Length;

            ParentWindow = mainWindow;

            //Инициализация GUI
            InitializeComponent();

            //Инициализация списка
            InitializeContent(day);
        }

        public void InitializeContent(DateTime day)
        {
            //Чистим панель
            StPaContent.Children.Clear();

            //Сохраним день
            Day = day;

            //Заполняем список
            if (Day.DayOfWeek != DayOfWeek.Sunday && Day.DayOfWeek != DayOfWeek.Saturday)
            {
                tblMetaInfo.Text = $"РАСПИСАНИЕ НА {day.ToString("dd/MM/yyyy")} - {UtilityApi.DayOfWeek(day).ToUpper()}";

                for (int i = 0; i < CountClasses; i++)
                {
                    var e = new EventListItem(ParentWindow,day, i);
                    StPaContent.Children.Add(e);
                }
            }
            else tblMetaInfo.Text = "ЗАПИСЬ ЗАКРЫТА - Сегодня нерабочий день!";
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            NexPrevDay(-1);
        }

        private void btNextWeek_Click(object sender, RoutedEventArgs e)
        {
            NexPrevDay(1);
        }

        private void NexPrevDay(int multiplier)
        {
            var day = Day;

            if (multiplier == 1)
            {
                switch (day.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        day = day.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        day = day.AddDays(2);
                        break;
                    case DayOfWeek.Friday:
                        day = day.AddDays(3);
                        break;
                    default:
                        day = day.AddDays(1);
                        break;
                }
            }
            else
            {
                switch (day.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        day = day.AddDays(2 * multiplier);
                        break;
                    case DayOfWeek.Saturday:
                        day = day.AddDays(1 * multiplier);
                        break;
                    case DayOfWeek.Monday:
                        day = day.AddDays(3 * multiplier);
                        break;
                    default:
                        day = day.AddDays(1 * multiplier);
                        break;
                }
            }
            InitializeContent(day);
        }

        public IEventItem GetEventItem(TimeSpan timeOffset)
        {
            return null;
        }
    }
}
