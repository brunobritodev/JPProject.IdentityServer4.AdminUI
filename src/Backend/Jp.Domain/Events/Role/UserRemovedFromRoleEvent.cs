using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class UserRemovedFromRoleEvent : Event
    {
        public string Name { get; }
        public string Username { get; }

        public UserRemovedFromRoleEvent(string name, string username)
        {
            AggregateId = name;
            Name = name;
            Username = username;
        }
    }
}