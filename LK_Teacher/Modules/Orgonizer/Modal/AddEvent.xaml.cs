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
using System.Windows.Shapes;
using LK_Teacher.Modules;
using LK_Teacher.Modules.Utility;

namespace LK_Teacher.Assets
{
    /// <summary>
    /// Логика взаимодействия для AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        private IEventItem eventGridItem;

        public AddEvent(Object egi)
        {
            InitializeComponent();
            this.eventGridItem = (IEventItem)egi;
            labDayWeek.Content = eventGridItem.GetNameDay();
            labDate.Content = eventGridItem.DayOfEvent.ToString("dd/MM/yyyy HH:mm");
            Console.WriteLine(this.eventGridItem.DayOfEvent.DayOfWeek);
        }

        private void BtAddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (DBApi.IsConnection)
            {
                int id = DBApi.AddNewEvent(tbTitle.Text, eventGridItem.DayOfEvent, tbDescription.Text, cbTypeEvent.SelectedIndex);
                if (id != -1)
                {
                    eventGridItem.TypeOfEvent = cbTypeEvent.SelectedIndex;
                    eventGridItem.IdEvent = id;
                    string nameStyleClass = "";
                    switch (cbTypeEvent.SelectedIndex)
                    {
                        case 1:
                            nameStyleClass = "ClassButton";
                            break;
                        case 2:
                            nameStyleClass = "СonferenceButton";
                            break;
                        case 3:
                            nameStyleClass = "EventButton";
                            break;
                    }

                    eventGridItem.btAction.Style = (Style)eventGridItem.TryFindResource(nameStyleClass);
                    eventGridItem.btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)(eventGridItem.PlusButtonClick));
                    eventGridItem.btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(eventGridItem.EventButtonClick));
                    eventGridItem.tblTitle.Text = tbTitle.Text;
                    this.Close();
                }
            }
        }


        private void CheckValidData()
        {
            if (tbTitle.Text.Length < 5 || cbTypeEvent.SelectedIndex <= 0 || tbDescription.Text.Length < 5)
            {
                btAddEvent.IsEnabled = false;
                if (labValid != null)
                {
                    btAddEvent.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#333"));
                    labValid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                btAddEvent.IsEnabled = true;
                btAddEvent.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fff"));
                labValid.Visibility = Visibility.Hidden;
            }
        }



        private void cbTypeEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckValidData();
        }

        private void tbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValidData();
        }

        private void tbTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValidData();
        }
    }
}
