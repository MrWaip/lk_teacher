using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Event
{
    class EventTimerArgs
    {
        public TimeSpan CurrentTimeEvent;
        public TimeSpan TimeToEnd;
        public string Status;
    }

    internal class EventTimer : PubSubEvent<EventTimerArgs>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly EventTimer _event;

        static EventTimer()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<EventTimer>();
        }

        public static void ClearInstance()
        {
            _event.Subscriptions.Clear();
        }

        public static EventTimer Instance
        {
            get { return _event; }
        }

    }
}
