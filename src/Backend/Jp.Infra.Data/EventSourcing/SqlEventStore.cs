using Jp.Domain.Core.Events;
using Jp.Domain.Core.StringUtils;
using Jp.Domain.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Jp.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ISystemUser _systemUser;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, ISystemUser systemUser)
        {
            _eventStoreRepository = eventStoreRepository;
            _systemUser = systemUser;
        }

        public Task Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            if (theEvent.Message.IsMissing())
                theEvent.Message = theEvent.MessageType.AddSpacesToSentence().Replace("Event", string.Empty).Trim();

            var storedEvent = new StoredEvent(
               theEvent.MessageType,
               theEvent.EventType,
               theEvent.Message,
               _systemUser.GetLocalIpAddress(),
               _systemUser.GetRemoteIpAddress(),
               serializedData)
                .SetUser(_systemUser.Username).SetAggregate(theEvent.AggregateId);

            return _eventStoreRepository.Store(storedEvent);
        }
    }
}