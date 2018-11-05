using LK_Teacher.Assets;
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
using LK_Teacher.Moduls;
using System.Windows.Threading;
using LK_Teacher.Moduls.Content;
using LK_Teacher.Moduls.API;
using System.Collections;

namespace LK_Teacher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Данные пользователя
        public Hashtable DataUser { get; private set; }

        //Модуль контента
        private UserControl ContentModule;

        //Время текущуго события
        private TimeSpan CurrentTimeOfClass;

        //Делегат сигнатуры метода обработчика событий
        public delegate void ActiveEventHandler(DateTime dayEvent);
        
        //Событие таймера
        public event ActiveEventHandler TurnOnOff;

        public MainWindow(Hashtable dataUser)
        {
            InitializeComponent();

            DataUser = dataUser;

            //Основная иницализация с формы на сегодня
            ContentModule = new EventListForm(this, DateTime.Today);
            Grid.SetColumn(ContentModule, 1);
            Grid.SetRow(ContentModule, 0);
            mainGrid.Children.Add(ContentModule);
            

            //InitializeEventGrid();

            //Задаем таймер
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(500);
            TimerUpdater();
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimerUpdater();
        }

        private void TimerUpdater()
        {
            if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday &&
                DateTime.Today.DayOfWeek != DayOfWeek.Sunday &&
                DateTime.Now <= UtilityApi.LastTimeOfClassToday().Add(UtilityApi.EndLastClass)
                )
            {
                if (CurrentTimeOfClass.TotalMinutes == 0)
                {
                    CurrentTimeOfClass = UtilityApi.TimeOfCurrentClass();
                }

                if (TurnOnOff != null)
                {
                    TurnOnOff(DateTime.Today.Add(CurrentTimeOfClass));
                }

                if (CurrentTimeOfClass.TotalMinutes != 0)
                {
                    labClockTitle.Content = "До конца события:";
                    lbClock.Visibility = Visibility.Visible;
                    labTimeToEnd.Visibility = Visibility.Visible;
                    TimeSpan toEndClass = CurrentTimeOfClass.Add(UtilityApi.DurationClass) - DateTime.Now.TimeOfDay;
                    labTimeToEnd.Content = toEndClass.ToString(@"hh\:mm\:ss");
                    if (toEndClass.TotalSeconds <= 0)
                    {
                        CurrentTimeOfClass = new TimeSpan();
                    }
                }

                if (CurrentTimeOfClass.TotalMinutes == 0)
                {
                    labClockTitle.Content = "Перерыв";
                    CurrentTimeOfClass = UtilityApi.TimeOfCurrentClass();
                    lbClock.Visibility = Visibility.Collapsed;
                    labTimeToEnd.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                labClockTitle.Content = "Занятия окончены!";
                lbClock.Visibility = Visibility.Collapsed;
                labTimeToEnd.Visibility = Visibility.Collapsed;

            }
        }

        private void btWeek_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Remove(ContentModule);
            ContentModule = new EventGridForm(this, UtilityApi.GetMondayOfCurrentWeek());
            Grid.SetColumn(ContentModule, 1);
            Grid.SetRow(ContentModule, 0);
            mainGrid.Children.Add(ContentModule);
        }

        private void btToday_Click(object sender, RoutedEventArgs e)
        {
            ViewDayEvent(DateTime.Today);
        }

        public void ViewDayEvent(DateTime day)
        {
            mainGrid.Children.Remove(ContentModule);
            ContentModule = new EventListForm(this, day);
            Grid.SetColumn(ContentModule, 1);
            Grid.SetRow(ContentModule, 0);
            mainGrid.Children.Add(ContentModule);
        }
    }
}
