using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Moduls.API
{
    static class UtilityApi
    {

        public static TimeSpan[] TimeOfClasses = { new TimeSpan(8, 30, 0), new TimeSpan(10, 10, 0), new TimeSpan(12, 10, 0), new TimeSpan(13, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(17, 30, 0) };

        //public static TimeSpan[] TimeOfClasses = { new TimeSpan(19, 8, 0) };

        public static readonly TimeSpan DurationClass = new TimeSpan(1,30,0);

        public static readonly TimeSpan EndLastClass = new TimeSpan(1, 30, 1);

        public static string DayOfWeekToday()
        {
            return DayOfWeek(DateTime.Today);
        }
        public static string DayOfWeek(DateTime date)
        {
            switch ((int)date.DayOfWeek)
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
                case 6:
                    return "Суббота";
                case 7:
                    return "Воскресенье";
            }
            return null;
        }

        //Возвращает дату (DateTime)  понедельника текущей недели 
        public static DateTime GetMondayOfCurrentWeek()
        {
            var date = DateTime.Today;
            while (date.DayOfWeek != System.DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        //Возвращает дату (DateTime)  понедельника недели указанного дня
        public static DateTime GetMondayOfWeek(DateTime day)
        {
            while (day.DayOfWeek != System.DayOfWeek.Monday)
            {
                day = day.AddDays(-1);
            }
            return day;
        }

        public static DateTime LastTimeOfClassToday()
        {
            DateTime lastTimeOfClassToday = DateTime.Today.Add(TimeOfClasses.Last());
            return lastTimeOfClassToday;
        }

        public static TimeSpan TimeOfCurrentClass()
        {
            foreach (TimeSpan ts in TimeOfClasses)
            {
                if (DateTime.Now.TimeOfDay >= ts && DateTime.Now.TimeOfDay <= ts.Add(DurationClass))
                {
                    return ts;
                }
            }
            return new TimeSpan();
        }
    }
}
