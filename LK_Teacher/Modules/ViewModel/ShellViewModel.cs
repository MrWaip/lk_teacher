using Prism.Mvvm;
using System;
using LK_Teacher.Modules.View;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LK_Teacher.Utility;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Event;
using LK_Teacher.Modules.Models;

namespace LK_Teacher.Modules.Shell
{
    public class ShellViewModel : BindableBase
    {
        private readonly Hashtable UserData;

        private ShellModel _SModel; 

        //Событие таймера
        //public event ActiveEventHandler TurnOnOff;

        public ShellViewModel(Hashtable userData)
        {
            _SModel = new ShellModel(userData);

            UserData = userData;

            EventDayReference.Instance.Subscribe(FollowToDay);

            if ((string)UserData["status_profile_teacher"] == UserModel.ACTIVE_PROFILE)
            {
                ContentModule = new EventListView(DateTime.Today);
            }
            else
            {
                ContentModule = new ProfileView();
            }
        }

        private void FollowToDay(DateTime day_event)
        {
            ContentModule = new EventListView(day_event);
        }

        //Сойства --------------------------------------------------------------------------

        private UserControl _ContentModul;
        public UserControl ContentModule
        {
            get { return _ContentModul; }
            set
            {
                _ContentModul = value;
                RaisePropertyChanged("ContentModule");
            }
        }


        //Команды --------------------------------------------------------------------------

        private RelayCommand _WeekViewCommand;
        public RelayCommand WeekViewCommand
        {
            get
            {
                return _WeekViewCommand ??
                  (_WeekViewCommand = new RelayCommand(obj =>
                  {
                      ContentModule = new EventGridView(UtilFunctions.GetMondayOfCurrentWeek());
                  }));
            }
        }

        private RelayCommand _DayViewCommand;
        public RelayCommand DayViewCommand
        {
            get
            {
                return _DayViewCommand ??
                  (_DayViewCommand = new RelayCommand(obj =>
                  {
                      ContentModule = new EventListView(DateTime.Today);
                  }));
            }
        }
    }
}
