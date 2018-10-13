using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class ResetLinkGeneratedEvent : Event
    {
        public string Email { get; }
        public string Username { get; }

        public ResetLinkGeneratedEvent(Guid aggregateId, string email, string username)
        {
            AggregateId = aggregateId.ToString();
            Email = email;
            Username = username;
        }
    }
}