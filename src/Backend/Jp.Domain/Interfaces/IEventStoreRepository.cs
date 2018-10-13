using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Interfaces
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<List<StoredEvent>> All(string username);
    }
}