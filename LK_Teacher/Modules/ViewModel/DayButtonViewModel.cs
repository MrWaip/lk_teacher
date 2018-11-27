using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Utility;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.ViewModel
{
    class DayButtonViewModel :BindableBase
    {
        private DateTime _DayEvent;
        public string DayEvent
        {
            get
            {
                return _DayEvent.ToLongDateString();
            }
        }

        public string DayOfWeek
        {
            get
            {
                return UtilFunctions.DayOfWeek(_DayEvent);
            }
        }


        public DayButtonViewModel(DateTime day_event)
        {
            _DayEvent = day_event;
        }

        private RelayCommand _FollowCommand;
        public RelayCommand FollowCommand
        {
            get
            {
                return _FollowCommand ??
                  (_FollowCommand = new RelayCommand(obj =>
                  {
                      EventDayReference.Instance.Publish(_DayEvent);
                  }));
            }
        }
    }
}
