using LK_Teacher.Modules;
using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.ViewModel;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LK_Teacher.Modules.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeEvent.xaml
    /// </summary>
    public partial class ChangeEvent : Window
    {
        public ChangeEvent(int id_event)
        {
            DataContext = new ChangeEventViewModel(id_event);
            InitializeComponent();
        }
    }
}
