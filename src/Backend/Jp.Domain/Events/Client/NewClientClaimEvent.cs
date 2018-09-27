using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientClaimEvent : Event
    {
        public int Id { get; }
        public string ClientId { get; }
        public string Type { get; }
        public string Value { get; }

        public NewClientClaimEvent(int id, string clientId, string type, string value)
        {
            Id = id;
            ClientId = clientId;
            Type = type;
            Value = value;
        }
    }
}