using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserRegisteredEvent : Event
    {
        public string Username { get; }
        public string UserEmail { get; }

        public UserRegisteredEvent(Guid aggregateId, string userName, string userEmail)
        {
            AggregateId = aggregateId.ToString();
            Username = userName;
            UserEmail = userEmail;
        }
    }
}