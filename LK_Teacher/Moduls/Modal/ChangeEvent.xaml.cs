using LK_Teacher.Moduls;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LK_Teacher.Assets
{
    /// <summary>
    /// Логика взаимодействия для ChangeEvent.xaml
    /// </summary>
    public partial class ChangeEvent : Window
    {
        private IEventItem eventGridItem;

        private ChangeEvent()
        {
            InitializeComponent();
        }

        internal ChangeEvent(IEventItem egi, Hashtable baseValues = null):this()
        {
            this.eventGridItem = egi;
            labDayWeek.Content = eventGridItem.GetNameDay();
            labDate.Content = eventGridItem.DayOfEvent.ToString("dd/MM/yyyy HH:mm");

            if(baseValues == null) baseValues = Api.GetDataEvent(eventGridItem.IdEvent);

            cbTypeEvent.SelectedIndex = Convert.ToInt32(baseValues["type_event"]);
            tbTitle.Text = baseValues["title_event"].ToString();
            tbDescription.Text = baseValues["description_event"].ToString();

            Console.WriteLine(this.eventGridItem.DayOfEvent.DayOfWeek);
        }

        private void CheckValidData()
        {
            if (tbTitle.Text.Length < 5 || cbTypeEvent.SelectedIndex <= 0 || tbDescription.Text.Length < 5)
            {
                btChangeEvent.IsEnabled = false;
                if (labValid != null)
                {
                    btChangeEvent.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#333"));
                    labValid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                btChangeEvent.IsEnabled = true;
                btChangeEvent.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fff"));
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

        private void btChangeEvent_Click(object sender, RoutedEventArgs e)
        {
            if (Api.IsConnection)
            {
                Api.UpdateEvent(eventGridItem.IdEvent,tbTitle.Text, eventGridItem.DayOfEvent, tbDescription.Text, cbTypeEvent.SelectedIndex);
                eventGridItem.TypeOfEvent = cbTypeEvent.SelectedIndex;
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
                eventGridItem.btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(eventGridItem.EventButtonClick));
                eventGridItem.tblTitle.Text = tbTitle.Text;
                this.Close();
                
            }
        }
    }
}
