using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserClaimRemovedEvent : Event
    {
        public string Username { get; }
        public string Type { get; }

        public UserClaimRemovedEvent(string username, string type)
        {
            Username = username;
            Type = type;
        }
    }
}