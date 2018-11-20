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
        public EventForm()
        {
            DataContext = new EventFormViewModel();
            InitializeComponent();
        }
    }
}
