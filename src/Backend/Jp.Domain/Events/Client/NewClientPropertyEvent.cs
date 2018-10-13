using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class NewClientPropertyEvent : Event
    {
        public int Id { get; }
        public string Key { get; }
        public string Value { get; }

        public NewClientPropertyEvent(int id, string clientId, string key, string value)
        {
            Id = id;
            AggregateId = clientId;
            Key = key;
            Value = value;
        }
    }

}