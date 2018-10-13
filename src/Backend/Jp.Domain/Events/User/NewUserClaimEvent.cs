using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class NewUserClaimEvent : Event
    {
        public string Username { get; }
        public string Type { get; }
        public string Value { get; }

        public NewUserClaimEvent(string username, string type, string value)
        {
            AggregateId = username;
            Username = username;
            Type = type;
            Value = value;
        }
    }
}