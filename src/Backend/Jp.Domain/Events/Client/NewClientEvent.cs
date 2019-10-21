using Jp.Domain.Commands.Clients;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientEvent : Event
    {
        public ClientType ClientType { get; }
        public string ClientName { get; }

        public NewClientEvent(string clientId, ClientType clientType, string clientName)
            : base(EventTypes.Success)
        {
            AggregateId = clientId;
            ClientType = clientType;
            ClientName = clientName;
        }
    }
}