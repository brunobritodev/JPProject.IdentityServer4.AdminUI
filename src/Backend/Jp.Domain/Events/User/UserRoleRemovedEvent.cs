using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserRoleRemovedEvent : Event
    {
        public string Username { get; }
        public string Role { get; }

        public UserRoleRemovedEvent(Guid aggregateId, string username, string role)
        {
            AggregateId = aggregateId.ToString();
            Username = username;
            Role = role;
        }
    }
}