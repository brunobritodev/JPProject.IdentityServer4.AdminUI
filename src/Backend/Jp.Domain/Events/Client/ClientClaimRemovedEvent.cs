using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientClaimRemovedEvent : Event
    {
        public int Id { get; }

        public ClientClaimRemovedEvent(int id, string clientId)
            : base(EventTypes.Success)
        {
            Id = id;
            AggregateId = clientId;
        }
    }
}