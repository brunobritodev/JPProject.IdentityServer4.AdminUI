using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientPropertyRemovedEvent : Event
    {
        public int Id { get; }
        public string ClientId { get; }

        public ClientPropertyRemovedEvent(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }
    }

}