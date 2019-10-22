using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientSecretRemovedEvent : Event
    {
        public int SecretId { get; }

        public ClientSecretRemovedEvent(int id, string clientId)
            : base(EventTypes.Success)
        {
            SecretId = id;
            AggregateId = clientId;
        }
    }
}