using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.View;
using LK_Teacher.Modules.ViewModel;
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
    /// Логика взаимодействия для EventGridView.xaml
    /// </summary>
    public partial class EventGridView : UserControl
    {
        public EventGridView(DateTime monday)
        {
            DataContext = new EventGridViewModel(monday);
            InitializeComponent();
        }
    }
}
