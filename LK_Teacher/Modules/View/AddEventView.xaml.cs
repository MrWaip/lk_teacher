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
using LK_Teacher.Modules.ViewModel;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Window
    {
        /// <summary>
        /// Инициализируем модальное окно типа AddEventView, для добавления нового события на указанню дату и время
        /// </summary>
        /// <param name="date_event">Дата и время на которое будет добавлено событие</param>
        public AddEventView(DateTime date_event)
        {
            InitializeComponent();
            DataContext = new AddEventViewModel(date_event);
        }
    }
}
