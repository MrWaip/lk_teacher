using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Event
{
    class EventListItemHandlerArgs
    {
        public enum TypeOfAction
        {
            Add,Change
        }

        public int IdEvent { get; set; }
        public TypeOfAction Action { get; set; }
    }

    class EventContainerUpdater : PubSubEvent
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly EventContainerUpdater _event;

        static EventContainerUpdater()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<EventContainerUpdater>();
        }

        public static EventContainerUpdater Instance
        {
            get { return _event; }
        }
    }
}
