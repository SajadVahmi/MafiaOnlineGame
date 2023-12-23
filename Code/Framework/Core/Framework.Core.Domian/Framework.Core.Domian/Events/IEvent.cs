using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Domian.Events
{
    public interface IEvent
    {
        string EventId { get; }
        DateTimeOffset WhenItHappened { get; }
    }
}
