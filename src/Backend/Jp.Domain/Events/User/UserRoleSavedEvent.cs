using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class UserRoleSavedEvent : Event
    {
        public string Username { get; }
        public string Role { get; }

        public UserRoleSavedEvent(Guid aggregateId, string username, string role)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Username = username;
            Role = role;
        }
    }
}