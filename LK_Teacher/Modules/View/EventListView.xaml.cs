﻿using LK_Teacher.Modules.Utility;
using LK_Teacher.Modules.Interface;
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
using LK_Teacher.Modules.ViewModel;

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
