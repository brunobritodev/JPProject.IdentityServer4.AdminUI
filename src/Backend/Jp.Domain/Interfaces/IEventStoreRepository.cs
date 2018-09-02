using System;
using System.Collections.Generic;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Interfaces
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IEnumerable<StoredEvent> All(Guid aggregateId);
    }
}