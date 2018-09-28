using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientEvent : Event
    {
        public string ClientId { get; }
        public ClientType ClientType { get; }
        public string ClientName { get; }

        public NewClientEvent(string clientId, ClientType clientType, string clientName)
        {
            ClientId = clientId;
            ClientType = clientType;
            ClientName = clientName;
        }
    }
}