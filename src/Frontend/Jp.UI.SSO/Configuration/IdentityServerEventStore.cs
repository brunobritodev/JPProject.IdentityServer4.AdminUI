using IdentityServer4.Services;
using Jp.Domain.Core.Events;
using Jp.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Is4Event = IdentityServer4.Events.Event;

namespace Jp.UI.SSO.Configuration
{
    public class IdentityServerEventStore : IEventSink
    {
        private readonly IEventStore _eventStore;
        private readonly ISystemUser _user;

        public IdentityServerEventStore(IEventStore eventStore, ISystemUser user)
        {
            _eventStore = eventStore;
            _user = user;
        }
        public Task PersistAsync(Is4Event evt)
        {
            Console.WriteLine("{0} ({1}), Details: {2}",
                evt.Name,
                evt.Id,
                evt.ToString());


            var es = new StoredEvent(
                evt.Id.ToString(),
                evt.Category,
                (EventTypes)(int)evt.EventType,
                evt.Name,
                evt.LocalIpAddress,
                evt.RemoteIpAddress,
                    evt.ToString()
                ).ReplaceTimeStamp(evt.TimeStamp);

            if (_user.IsAuthenticated())
                es.SetUser(_user.Username);

            _eventStore.Save(es);

            return Task.CompletedTask;
        }
    }
}
