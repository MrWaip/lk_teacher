using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LK_Teacher.Modules.Models
{
    class ShellModel : BindableBase
    {
        private DispatcherTimer _Timer;

        private TimeSpan CurrentTimeEvent;
        private TimeSpan TimeToEnd;

        public ShellModel(Hashtable user_data)
        {
            UserModel.IdTeacher = Convert.ToInt32(user_data["id_teacher"]);
            UserModel.StatusProfileTeacher = user_data["status_profile_teacher"] as string;
            UserModel.DirectionTeacher = Convert.ToInt32( user_data["id_direction_teacher"] as string);


            ////Задаем таймер
            _Timer = new DispatcherTimer();
            _Timer.Tick += new EventHandler(TimerTick);
            _Timer.Interval = new TimeSpan(500);
            _Timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            string status;

            if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday &&
                DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
            {

                if (DateTime.Now <= UtilFunctions.LastTimeOfClassToday().Add(UtilFunctions.EndLastClass))
                {
                    CurrentTimeEvent = UtilFunctions.TimeOfCurrentEvent();

                    if (CurrentTimeEvent.TotalSeconds == 0)
                    {
                        status = "pause";
                        TimeToEnd = new TimeSpan();
                    }
                    else
                    {
                        status = "event_comming";
                        TimeToEnd = CurrentTimeEvent.Add(UtilFunctions.DurationEvent) - DateTime.Now.TimeOfDay;
                    }
                }
                else
                {
                    status = "day_end";
                }

                var param = new EventTimerArgs()
                {
                    Status = status,
                    CurrentTimeEvent = this.CurrentTimeEvent,
                    TimeToEnd = this.TimeToEnd
                };

                try
                {
                    EventTimer.Instance.Publish(param);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); };
            }
        }
    }
}
