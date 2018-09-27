using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientClaimRemovedEvent : Event
    {
        public int Id { get; }
        public string ClientId { get; }

        public ClientClaimRemovedEvent(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }
    }
}