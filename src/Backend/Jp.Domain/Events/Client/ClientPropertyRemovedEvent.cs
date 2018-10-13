using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientPropertyRemovedEvent : Event
    {
        public int PropertyId { get; }

        public ClientPropertyRemovedEvent(int id, string clientId)
        {
            PropertyId = id;
            AggregateId = clientId;
        }
    }

}