using LK_Teacher.Event;
using LK_Teacher.Modules.Models;
using LK_Teacher.Utility;
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
    class EventListItemViewModel : BindableBase
    {
        private EventListItemModel _ELIModel;

        public string TitleMetaInfo
        {
            get
            {
                if (_ELIModel.TitleMetaInfo is null) return "Пусто";
                else return _ELIModel.TitleMetaInfo;
            }
        }

        public string DateMetaInfo
        {
            get
            {
                return _ELIModel.DateMetaInfo;
            }
        }

        public string ActionButtonStyle
        {
            get { return _ELIModel.ActionButtonStyle; }
        }

        public string LabelStatusStyle
        {
            get { return _ELIModel.LabelStatusStyle; }
        }

        public string TitleStyle
        {
            get { return _ELIModel.TitleStyle; }
        }

        public bool IsEventSet
        {
            get
            {
                return _ELIModel.IsEventSet;
            }
        }

        private string _ShowForm = "Colapsed";
        public string ShowForm
        {
            get { return _ShowForm; }
            set
            {
                _ShowForm = value;
                RaisePropertyChanged("ShowForm");
            }
        }

        public EventListItemViewModel(DateTime day_event, int number_event)
        {
            _ELIModel = new EventListItemModel(day_event, number_event);
            _ELIModel.PropertyChanged += ModelPropertyChanged
;
        }

        private RelayCommand _ActionCommand;
        public RelayCommand ActionCommand
        {
            get
            {
                return _ActionCommand ??
                  (_ActionCommand = new RelayCommand(obj =>
                  {
                      if (IsEventSet)
                      {
                          //выводим ИНФУ 
                          _ELIModel.ShowInfoEvent();

                      }
                      else
                      {
                          //
                      }
                  }));
            }
        }

        public void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
