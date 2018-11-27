using LK_Teacher.Event;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LK_Teacher.Modules.Models
{
    class EventGridModel : BindableBase
    {
        //Константы
        readonly int Rows;
        readonly int Cols;
        readonly int CountClasses;
        readonly int LengthWorkWeek;

        private ObservableCollection<object> _EventGrid;
        public ObservableCollection<object> EventGrid
        {
            get
            {
                return _EventGrid;
            }
            set
            {
                _EventGrid = value;
            }
        }

        private DateTime _DateStartWeek;

        private string _TitleString;
        public string TitleString
        {
            get { return _TitleString; }
            set
            {
                _TitleString = value;
                RaisePropertyChanged("TitleString");
            }
        }

        public EventGridModel(DateTime monday)
        {
            EventContainerUpdater.Instance.Subscribe(Update);

            //Инициализация констант так сказать
            CountClasses = UtilFunctions.TimesOfEvents.Length;
            Rows = CountClasses + 1;
            LengthWorkWeek = UtilFunctions.LengthWorkWeek;
            Cols = 1 + LengthWorkWeek;

            EventGrid = new ObservableCollection<Object>();

            //LabelTimeGrid = new ObservableCollection<Label>();

            InitializeEventGrid(monday);
        }

        public void InitializeEventGrid(DateTime monday)
        {
            _DateStartWeek = monday;

            //Предварительная чистка
            EventGrid.Clear();

            //Заголовок
            TitleString = $"РАСПИСАНИЕ НА {monday.ToString("dd/MM/yyyy")} - {monday.AddDays(6).ToString("dd/MM/yyyy")}";

            //Инициализация EventGridItemView

            for (int i = 0; i < LengthWorkWeek; i++)
            {
                var bt = new DayButtonView(monday.AddDays(i));
                EventGrid.Add(bt);
                Grid.SetColumn(bt, i + 1);
                Grid.SetRow(bt, 0);
            }

            //Сорри, но не целесообразно писать View для этого элемента
            for (int i = 0; i < CountClasses; i++)
            {
                var l = new Label()
                {
                    Content = new DateTime().Add(UtilFunctions.TimesOfEvents[i]).ToString("HH:mm"),
                    Style = Application.Current.FindResource("TimeLabel") as Style
                };
                Grid.SetColumn(l, 0);
                Grid.SetRow(l, i + 1);
                EventGrid.Add(l);
            }

            for (int i = 0; i < LengthWorkWeek; i++)
            {
                for (int j = 0; j < CountClasses; j++)
                {
                    var e = new EventGridItemView(monday, i, j);
                    EventGrid.Add(e);
                }
            }
        }

        private void Update()
        {
            InitializeEventGrid(_DateStartWeek);
        }

        public void ChangeWeek(int offset)
        {
            InitializeEventGrid(_DateStartWeek.AddDays(offset));
        }
    }
}
