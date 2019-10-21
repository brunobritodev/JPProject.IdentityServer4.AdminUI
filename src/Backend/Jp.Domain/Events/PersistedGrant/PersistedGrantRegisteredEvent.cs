using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.PersistedGrant
{
    public class PersistedGrantRemovedEvent : Event
    {
        public string Key { get; }

        public PersistedGrantRemovedEvent(string key)
            : base(EventTypes.Success)
        {
            Key = key;
            AggregateId = key;
        }
    }
}