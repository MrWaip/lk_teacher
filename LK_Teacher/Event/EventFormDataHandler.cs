using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Event
{
    internal class EventFormDataHandler : PubSubEvent<int>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly EventFormDataHandler _event;

        static EventFormDataHandler()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<EventFormDataHandler>();
        }

        public static EventFormDataHandler Instance
        {
            get { return _event; }
        }

    }
}
