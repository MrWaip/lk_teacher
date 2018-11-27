using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Event
{
    internal class EventDayReference : PubSubEvent<DateTime>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly EventDayReference _event;

        static EventDayReference()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<EventDayReference>();
        }

        public static EventDayReference Instance
        {
            get { return _event; }
        }

    }
}
