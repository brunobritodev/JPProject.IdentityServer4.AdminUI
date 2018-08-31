using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class UserRegisteredEvent : Event
    {
        public string Username { get; }
        public string UserEmail { get; }

        public UserRegisteredEvent(Guid aggregateId, string userName, string userEmail)
        {
            AggregateId = aggregateId;
            Username = userName;
            UserEmail = userEmail;
        }
    }
}