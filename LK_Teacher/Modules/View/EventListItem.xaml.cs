using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using LK_Teacher.Modules.ViewModel;
using System;
using System.Collections;
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

    public partial class EventListItem : UserControl
    {
        /// <summary>
        /// Инициализируем элемент коллекции EventList
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="evf"></param>
        /// <param name="dayEvent">День события</param>
        /// <param name="number_class">Номмер события. Смотри UtilFunctions.</param>
        public EventListItem(DateTime dayEvent, int number_class)
        {
            InitializeComponent();
            DataContext = new EventListItemViewModel(dayEvent, number_class);
        }
    }
}
