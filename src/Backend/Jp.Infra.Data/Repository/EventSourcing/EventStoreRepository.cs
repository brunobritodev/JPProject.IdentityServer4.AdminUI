using Jp.Domain.Core.Events;
using Jp.Domain.Core.StringUtils;
using Jp.Domain.Core.ViewModels;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jp.Infra.Data.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }

        public Task<List<StoredEvent>> All(string username)
        {
            return (from e in _context.StoredEvent where e.User == username orderby e.Timestamp descending select e).ToListAsync();
        }

        public async Task<List<StoredEvent>> GetEvents(string username, PagingViewModel paging)
        {
            List<StoredEvent> events = null;
            if (paging.Search.IsPresent())
                events = await _context.StoredEvent
                                    .Include(s => s.Details)
                                    .Where(EventFind(username, paging.Search))
                                    .OrderByDescending(o => o.Timestamp)
                                    .Skip(paging.Offset)
                                    .Take(paging.Limit).ToListAsync();
            else
                events = await _context.StoredEvent
                                    .Include(s => s.Details)
                                    .Where(w => w.User == username)
                                    .Skip(paging.Offset)
                                    .OrderByDescending(o => o.Timestamp)
                                    .Take(paging.Limit).ToListAsync();

            return events;
        }

        private static Expression<Func<StoredEvent, bool>> EventFind(string username, string search)
        {
            return w => (w.Message.Contains(search) ||
                        w.MessageType.Contains(search) ||
                        w.AggregateId.Contains(search)) &&
                        w.User == username;
        }

        public Task<int> Count(string username, string search)
        {
            return search.IsPresent() ? _context.StoredEvent.Where(EventFind(username, search)).CountAsync() : _context.StoredEvent.Where(w => w.User == username).CountAsync();
        }

        public async Task Store(StoredEvent theEvent)
        {
            await _context.StoredEvent.AddAsync(theEvent);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}