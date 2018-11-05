using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string strPattern = @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";

            return Regex.Replace(phoneNumber, "^" + strPattern + "$", "+7($2$3$4)$5$6$7-$8$9-$10$11");
        }

        //Валидация на SQL Инъекции

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

    }

}
