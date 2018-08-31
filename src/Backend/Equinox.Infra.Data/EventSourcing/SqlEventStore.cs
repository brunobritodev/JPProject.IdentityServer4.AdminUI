using Equinox.Domain.Core.Events;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace Equinox.Infra.Data.EventSourcing
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

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _systemUser.Username);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}