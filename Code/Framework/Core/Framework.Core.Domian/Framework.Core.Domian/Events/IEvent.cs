using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Domian.Events
{
    public interface IEvent
    {
        Guid EventId { get; }
        DateTimeOffset PublishDateTime { get; }
    }
}
