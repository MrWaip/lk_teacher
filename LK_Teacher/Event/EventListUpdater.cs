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

    class EventListUpdater : PubSubEvent
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly EventListUpdater _event;

        static EventListUpdater()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<EventListUpdater>();
        }

        public static EventListUpdater Instance
        {
            get { return _event; }
        }
    }
}
