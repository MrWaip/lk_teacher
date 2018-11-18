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

namespace LK_Teacher.Modules.Shell
{
    public class ShellViewModel : BindableBase
    {
        private readonly Hashtable UserData;

        //Делегат сигнатуры метода обработчика событий
        public delegate void ActiveEventHandler(DateTime dayEvent);
        //Событие таймера
        public event ActiveEventHandler TurnOnOff;

        public ShellViewModel(Hashtable userData)
        {
            UserData = userData;

            //Основная иницализация с формы на сегодня

            if ((string)UserData["status_profile_teacher"] == DBApi.ACTIVE_PROFILE)
            {
                ContentModule = new EventListView(DateTime.Today);
            }
            else
            {
                ContentModule = new ProfileView();
            }

            //Grid.SetColumn(ContentModule, 1);
            //Grid.SetRow(ContentModule, 0);
            //mainGrid.Children.Add(ContentModule);

            ////Задаем таймер
            //DispatcherTimer dispatcherTimer = new DispatcherTimer();
            //dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(500);
            //TimerUpdater();
            //dispatcherTimer.Start();
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
                      ContentModule = new EventGridForm(UtilFunctions.GetMondayOfCurrentWeek());
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
