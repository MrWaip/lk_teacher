using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK_Teacher.Modules.Interface
{
    interface IEventForm
    {
        IEventItem GetEventItem(TimeSpan timeOffset);
    }
}
