using System;
using System.Collections.Generic;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Interfaces
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}