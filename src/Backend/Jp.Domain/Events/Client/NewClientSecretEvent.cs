using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientSecretEvent : Event
    {
        public int Id { get; }
        public string Type { get; }
        public string Description { get; }

        public NewClientSecretEvent(int id, string clientId, string type, string description)
        {
            Id = id;
            AggregateId = clientId;
            Type = type;
            Description = description;
        }
    }
}