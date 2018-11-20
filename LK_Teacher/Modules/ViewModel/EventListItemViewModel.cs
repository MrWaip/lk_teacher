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

        public EventListItemModel.StatusViewEvent StatusViewEvent
        {
            get
            {
                return _ELIModel.GetStatusViewEvent;
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
                      switch (StatusViewEvent)
                      {
                          case EventListItemModel.StatusViewEvent.FINISHED_INSTALLED:
                              _ELIModel.ShowInfoEvent();
                              break;
                          case EventListItemModel.StatusViewEvent.NOT_FINISHED_INSTALLED:
                              _ELIModel.ShowInfoEvent();
                              break;
                          case EventListItemModel.StatusViewEvent.NOT_FINISHED_NOT_INSTALLED:
                              _ELIModel.AddEvent();
                              break;
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
