using Jp.Domain.Core.Events;
using Jp.Domain.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Domain.Interfaces
{
    public interface IEventStoreRepository : IDisposable
    {
        Task Store(StoredEvent theEvent);
        Task<List<StoredEvent>> All(string username);
        Task<List<StoredEvent>> GetEvents(string username, PagingViewModel paging);
        Task<int> Count(string username, string search);
    }
}