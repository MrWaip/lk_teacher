using LK_Teacher.Modules;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.ViewModel;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для EventForm.xaml
    /// </summary>
    public partial class EventForm : UserControl
    {
        //private IEventItem eventGridItem;
        //private Hashtable dataEvent;

        public EventForm()
        {
            DataContext = new EventFormViewModel();
            InitializeComponent();
        }

        //private void btDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    string message = "Вы действительно хотите удалить событие?";
        //    string caption = "Удалить";
        //    MessageBoxButton buttons = MessageBoxButton.OKCancel;
        //    MessageBoxImage icon = MessageBoxImage.Question;
        //    // Show message box
        //    MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon);
        //    if (result == MessageBoxResult.OK)
        //    {
        //        DBApi.DeleteEvent(eventGridItem.IdEvent);
        //        eventGridItem.IdEvent = -1;
        //        eventGridItem.TypeOfEvent = -1;
        //        eventGridItem.btAction.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)eventGridItem.EventButtonClick);
        //        string nameStyleClass;
        //        if (eventGridItem.DayOfEvent >= DateTime.Now)
        //        {
        //            eventGridItem.btAction.AddHandler(Button.ClickEvent, new RoutedEventHandler(eventGridItem.PlusButtonClick));
        //            nameStyleClass = "PlusButton";
        //        }
        //        else nameStyleClass = "Disable";



        //        eventGridItem.btAction.Style = (Style)eventGridItem.TryFindResource(nameStyleClass);
        //        eventGridItem.tblTitle.Text = "пусто";
        //        this.Visibility = Visibility.Hidden; 
        //    }
        //}
    }
}
