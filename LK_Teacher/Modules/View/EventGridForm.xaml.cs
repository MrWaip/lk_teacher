using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для GridEventForm.xaml
    /// </summary>
    public partial class EventGridForm : UserControl
    {
        //Константы
        readonly int Row;
        readonly int Col;
        readonly int CountClasses;
        readonly int WorkWeek;

        //Ссылка на окно родителя
        private ShellView ParenWindow;

        //Дата понедельника
        private DateTime Monday;

        public EventGridForm(DateTime monday)
        {
            //Инициализация констант так сказать
            CountClasses = UtilFunctions.TimeofEvents.Length;
            Row = CountClasses + 1;
            WorkWeek = 5;
            Col = 1 + WorkWeek;

            //Инициализация GUI
            InitializeComponent();

            //Инициализация контента

            InitializeContent(monday);
            
        }

        public void InitializeContent(DateTime monday)
        {
            //Сохраняем понедельник
            Monday = monday;

            //Предварительная чистка
            eventGid.Children.Clear();

            //Заголовок
            tblMetaInfo.Text = $"РАСПИСАНИЕ НА {monday.ToString("dd/MM/yyyy")} - {monday.AddDays(6).ToString("dd/MM/yyyy")} - МЕСЯЦ";

            //Инициализация EventGridItem

            for (int i = 0; i < WorkWeek; i++)
            {
                var button = new Button();
                button.Style = (Style)this.TryFindResource("DayButton");
                button.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)btDayButton_Click);
                button.AddHandler(Button.ClickEvent, new RoutedEventHandler(btDayButton_Click));
                DateTime dayOfWeek = monday.AddDays(i);
                button.Tag = dayOfWeek;
                button.Content = UtilFunctions.DayOfWeek(dayOfWeek);
                Grid.SetColumn(button, i + 1);
                Grid.SetRow(button, 0);
                eventGid.Children.Add(button);

            }
            for (int i = 0; i < CountClasses; i++)
            {
                var l = new Label();
                l.Content = new DateTime().Add(UtilFunctions.TimeofEvents[i]).ToString("HH:mm");
                l.Style = (Style)this.TryFindResource("TimeLabel");
                Grid.SetColumn(l, 0);
                Grid.SetRow(l, i + 1);
                eventGid.Children.Add(l);
            }

            for (int i = 0; i < WorkWeek; i++)
            {
                for (int j = 0; j < CountClasses; j++)
                {
                    var e = new EventGridItem(ParenWindow,eventForm,monday, i, j);
                    Grid.SetColumn(e, e.AbsoluteCol);
                    Grid.SetRow(e, e.AbsoluteRow);
                    eventGid.Children.Add(e);
                }
            }
        }

        private void btDayButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime day = ((DateTime)((Button)sender).Tag);
            //ParenWindow.ViewDayEvent(day);
        }

        private void btNextWeek_Click(object sender, RoutedEventArgs e)
        {
            InitializeContent(Monday.AddDays(7));
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            InitializeContent(Monday.AddDays(-7));
        }
    }
}
