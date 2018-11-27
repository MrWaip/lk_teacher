using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Models
{
    class UserModel
    {
        public const string ACTIVE_PROFILE = "active";

        public const string WAIT_PROFILE = "wait";

        public const string FIRST_TIME_PROFILE = "first_time";

        private static int _IdETeacher;
        public static int IdTeacher
        {
            get
            {
                return _IdETeacher;
            }
            set
            {
                _IdETeacher = value;
            }
        }

        private static string _StatusProfileTeacher;
        public static string StatusProfileTeacher
        {
            get { return _StatusProfileTeacher; }
            set { _StatusProfileTeacher = value; }
        }

        public static int DirectionTeacher { get => _DirectionTeacher; set => _DirectionTeacher = value; }
        private static int _DirectionTeacher;

    }
}
