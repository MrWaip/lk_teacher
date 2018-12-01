using LK_Teacher.Modules.ViewModel;
using System;
using System.Windows.Controls;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для EventList.xaml
    /// </summary>
    public partial class EventListView : UserControl
    {
        public EventListView(DateTime day)
        {
            InitializeComponent();
            DataContext = new EventListViewModel(day);
        }
    }
}
