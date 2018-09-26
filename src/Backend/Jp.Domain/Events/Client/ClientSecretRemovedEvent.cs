using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientSecretRemovedEvent : Event
    {
        public int Id { get; }
        public string ClientId { get; }

        public ClientSecretRemovedEvent(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }
    }
}