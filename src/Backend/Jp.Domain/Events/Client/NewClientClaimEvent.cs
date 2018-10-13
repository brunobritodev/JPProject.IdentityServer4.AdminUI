using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientClaimEvent : Event
    {
        public int Id { get; }
        public string Type { get; }
        public string Value { get; }

        public NewClientClaimEvent(int id, string clientId, string type, string value)
        {
            Id = id;
            AggregateId = clientId;
            Type = type;
            Value = value;
        }
    }
}