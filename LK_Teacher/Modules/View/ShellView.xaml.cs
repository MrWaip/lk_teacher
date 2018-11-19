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
using LK_Teacher.Modules;
using System.Windows.Threading;
using LK_Teacher.Modules.Utility;
using System.Collections;
using Prism.Regions;
using LK_Teacher.Modules.Shell;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {

        public ShellView(Hashtable userData)
        {
            InitializeComponent();
            DataContext = new ShellViewModel(userData);
        }

        //    private void dispatcherTimer_Tick(object sender, EventArgs e)
        //    {
        //        TimerUpdater();
        //    }

        //    private void TimerUpdater()
        //    {
        //        Label labClockTitle = (Label)ContentModule.FindName("labClockTitle");
        //        Label lbClock = (Label)ContentModule.FindName("lbClock");
        //        Label labTimeToEnd = (Label)ContentModule.FindName("labTimeToEnd");

        //        if(labClockTitle != null)

        //        if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday &&
        //            DateTime.Today.DayOfWeek != DayOfWeek.Sunday &&
        //            DateTime.Now <= UtilityApi.LastTimeOfClassToday().Add(UtilityApi.EndLastClass)
        //            )
        //        {

        //            if (CurrentTimeOfClass.TotalMinutes == 0)
        //            {
        //                CurrentTimeOfClass = UtilityApi.TimeOfCurrentClass();
        //            }

        //            if (TurnOnOff != null)
        //            {
        //                TurnOnOff(DateTime.Today.Add(CurrentTimeOfClass));
        //            }

        //            if (CurrentTimeOfClass.TotalMinutes != 0)
        //            {
        //                labClockTitle.Content = "До конца события:";
        //                lbClock.Visibility = Visibility.Visible;
        //                labTimeToEnd.Visibility = Visibility.Visible;
        //                TimeSpan toEndClass = CurrentTimeOfClass.Add(UtilityApi.DurationClass) - DateTime.Now.TimeOfDay;
        //                labTimeToEnd.Content = toEndClass.ToString(@"hh\:mm\:ss");
        //                if (toEndClass.TotalSeconds <= 0)
        //                {
        //                    CurrentTimeOfClass = new TimeSpan();
        //                }
        //            }

        //            if (CurrentTimeOfClass.TotalMinutes == 0)
        //            {
        //                labClockTitle.Content = "Перерыв";
        //                CurrentTimeOfClass = UtilityApi.TimeOfCurrentClass();
        //                lbClock.Visibility = Visibility.Collapsed;
        //                labTimeToEnd.Visibility = Visibility.Collapsed;
        //            }
        //        }
        //        else
        //        {
        //            labClockTitle.Content = "Занятия окончены!";
        //            lbClock.Visibility = Visibility.Collapsed;
        //            labTimeToEnd.Visibility = Visibility.Collapsed;

        //        }
        //    }

    }
}
