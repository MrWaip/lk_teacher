using LK_Teacher.Event;
using LK_Teacher.Modules.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LK_Teacher.Modules.ViewModel
{
    class EventFormViewModel :BindableBase
    {
        private EventFormModel _EFModel;

        public Visibility Visibility
        {
            get
            {
                if (_EFModel.IsSetEvent)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            }
        }

        public string TitleEvent
        {
            get { return _EFModel.TitleEvent; }
        }

        public EventFormViewModel()
        {
            _EFModel = new EventFormModel();
            _EFModel.PropertyChanged += ModelPropertyChanged;
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
