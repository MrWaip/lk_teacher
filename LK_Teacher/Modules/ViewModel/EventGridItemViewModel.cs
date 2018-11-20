using LK_Teacher.Modules.Models;
using LK_Teacher.Utility;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.ViewModel
{
    class EventGridItemViewModel : BindableBase
    {
        private EventGridItemModel _EGIModel;
        
        public int AbsoluteCol
        {
            get { return _EGIModel.AbsoluteCol; }
        }

        public int AbsoluteRow
        {
            get { return _EGIModel.AbsoluteRow; }
        }

        public string TitleEvent
        {
            get
            {
                if (_EGIModel.TitleEvent is null) return "Пусто";
                else return _EGIModel.TitleEvent;
            }
        }

        public string DateMetaInfo
        {
            get
            {
                return _EGIModel.DateOfEvent.ToLongDateString();
            }
        }

        public string ActionButtonStyle
        {
            get { return _EGIModel.ActionButtonStyle; }
        }

        public string LabelStatusStyle
        {
            get { return _EGIModel.LabelStatusStyle; }
        }

        public EventGridItemModel.StatusViewEvent StatusViewEvent
        {
            get
            {
                return _EGIModel.GetStatusViewEvent;
            }
        }

        public EventGridItemViewModel(DateTime monday, int relativeCol, int relativeRow)
        {
            _EGIModel = new EventGridItemModel(monday, relativeCol, relativeRow);
            _EGIModel.PropertyChanged += ModelPropertyChanged;
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
                          case EventGridItemModel.StatusViewEvent.FINISHED_INSTALLED:
                              _EGIModel.ShowInfoEvent();
                              break;
                          case EventGridItemModel.StatusViewEvent.NOT_FINISHED_INSTALLED:
                              _EGIModel.ShowInfoEvent();
                              break;
                          case EventGridItemModel.StatusViewEvent.NOT_FINISHED_NOT_INSTALLED:
                              _EGIModel.AddEvent();
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
