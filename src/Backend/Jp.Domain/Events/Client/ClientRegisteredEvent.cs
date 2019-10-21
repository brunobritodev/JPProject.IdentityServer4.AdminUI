using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientRemovedEvent : Event
    {
        public ClientRemovedEvent(string clientId)
            : base(EventTypes.Success)
        {
            AggregateId = clientId;
        }
    }
}