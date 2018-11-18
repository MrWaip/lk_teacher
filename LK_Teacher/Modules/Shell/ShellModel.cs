using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Moduls.Shell
{
    class ShellModel
    {

        //Данные пользователя
        public Hashtable UserData { get; private set; }

        //Модуль контента
        private UserControl ContentModule;

        //Время текущуго события
        private TimeSpan CurrentTimeOfClass;
    }
}
