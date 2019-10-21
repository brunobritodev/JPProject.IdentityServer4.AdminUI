using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class UserRegisteredEvent : Event
    {
        public string Username { get; }
        public string UserEmail { get; }

        public UserRegisteredEvent(Guid aggregateId, string userName, string userEmail)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Username = userName;
            UserEmail = userEmail;
        }
    }
}