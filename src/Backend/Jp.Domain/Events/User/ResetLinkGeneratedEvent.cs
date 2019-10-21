using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class ResetLinkGeneratedEvent : Event
    {
        public string Email { get; }
        public string Username { get; }

        public ResetLinkGeneratedEvent(Guid aggregateId, string email, string username)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Email = email;
            Username = username;
        }
    }
}