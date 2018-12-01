using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Utility
{
    static class UtilFunctions
    {
        //Расписание событий -  Время пар техникума
        public static TimeSpan[] TimesOfEvents = { new TimeSpan(8, 30, 0), new TimeSpan(10, 10, 0), new TimeSpan(12, 10, 0), new TimeSpan(13, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(17, 30, 0) };

        public static int LengthWorkWeek = 5;

        //public static TimeSpan[] TimesOfEvents = { new TimeSpan(17, 36, 0), new TimeSpan(19,29,0) };

        //Константное время одного события
        public static readonly TimeSpan DurationEvent = new TimeSpan(1, 30, 0);

        //Время завершения последнего события с поправкой на отключение системы
        public static readonly TimeSpan EndLastClass = new TimeSpan(1, 30, 1);

        /// <summary>
        /// Возвращает наименование типа события по номеру события.
        /// </summary>
        /// <param name="TypeOfEvent">
        /// Номер события:
        /// 1 = Занятие,
        /// 2 = Совещание,
        /// 3 = Мероприятие.
        /// </param>
        /// <returns></returns>
        public static string GetTypeEvent(int TypeOfEvent)
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

        //Возвращает день недели сегоднешнего дня
        public static string DayOfWeekToday()
        {
            return DayOfWeek(DateTime.Today);
        }

        //Возвращает день недели указанного дня
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

        //Возвращает Дату и время последней пары сегодня
        public static DateTime LastTimeOfClassToday()
        {
            DateTime lastTimeOfClassToday = DateTime.Today.Add(TimesOfEvents.Last());
            return lastTimeOfClassToday;
        }

        //Возвращает время текущего события если такое имеется
        public static TimeSpan TimeOfCurrentEvent()
        {
            foreach (TimeSpan ts in TimesOfEvents)
            {
                if (DateTime.Now.TimeOfDay >= ts && DateTime.Now.TimeOfDay <= ts.Add(DurationEvent))
                {
                    return ts;
                }
            }
            return new TimeSpan();
        }

        //Валидация полей --------------------------------------------------------------------

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string pnumber)
        {
            string strPattern = @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";

            if (Regex.IsMatch(pnumber, strPattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            string strPattern = @"(\+7|8|7|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";

            return Regex.Replace(phoneNumber, "^" + strPattern + "$", "+7($2$3$4)$5$6$7-$8$9-$10$11");
        }

        private static readonly Regex _regex = new Regex("[^0-9]+");

        public static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        //Валидация на SQL Инъекции ----------------------------------------------------------

        private static readonly Regex RegexId = new Regex(@"(^[_@\#a-zA-Z][_@\#\$a-zA-Z0-9]*(?:\.[_@\#\$a-zA-Z0-9]+)?$)|(^\[{1}[_@\#\$\.a-z A-Z0-9]{0,128}\]{1}$)", RegexOptions.Compiled);

        private static readonly Regex RegexInteger = new Regex(@"^[\d]+$", RegexOptions.Compiled);

        private static readonly Regex RegexValue = new Regex(@"(^(([']{1}([^'])*[']{1})|([^'])*)$)", RegexOptions.Compiled);

        public static string ValidateSqlId(string identifierName, bool allowEmptyStrings = false)
        {
            if (allowEmptyStrings && string.IsNullOrEmpty(identifierName))
            {
                return identifierName;
            }
            if (!RegexId.IsMatch(identifierName))
            {
                throw new ApplicationException(string.Format("'{0}' is not a valid SQL identifier name.", identifierName));
            }
            return identifierName;
        }

        public static string ValidateInteger(string oid, bool allowEmptyStrings = false)
        {
            if (!IsOidValid(oid, allowEmptyStrings))
            {
                throw new ApplicationException("Provided OID is not valid. The value should be integer.");
            }
            return oid;
        }

        private static bool IsOidValid(string oid, bool allowEmptyStrings)
        {
            return (allowEmptyStrings && string.IsNullOrEmpty(oid)) || RegexInteger.IsMatch(oid);
        }

        public static IEnumerable<string> ValidateIntegerList(IEnumerable<string> oidList, bool allowEmptyStrings = false)
        {
            if (!oidList.All(oid => IsOidValid(oid, allowEmptyStrings)))
            {
                throw new ApplicationException("Not a valid Integer List.");
            }
            return oidList;
        }

        public static string ValidateSqlValue(string value)
        {
            // Is it null?
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            // Is it a BOOLEAN?
            bool throwawayBool;
            if (bool.TryParse(value, out throwawayBool))
            {
                return value;
            }
            // Is it a number?
            double throwawayNumber;
            if (double.TryParse(value, out throwawayNumber))
            {
                return value;
            }
            if (RegexValue.IsMatch(value.Replace("''", string.Empty)))
            {
                return value;
            }
            throw new ApplicationException(string.Format("ValidateSqlValue: SQL value is not legitimate. Field: {0}", value));
        }

        public static string ValidateIntId(string id)
        {
            int result;
            return int.TryParse(id, out result) ? result.ToString(CultureInfo.InvariantCulture) : "";
        }

        public static int ValidateIntIdForSqlWhere(string id)
        {
            int result;
            return int.TryParse(id, out result) ? result : -1;
        }

        //Работа с файлами

        public static string OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            string result = "";

            if (openFileDialog.ShowDialog() == true)
                result = openFileDialog.FileName;
            return result;
        }

    }

}
