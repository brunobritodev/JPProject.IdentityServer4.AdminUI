using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Interfaces
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IEnumerable<StoredEvent> All(Guid aggregateId);
    }
}