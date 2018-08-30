using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class ResetLinkGeneratedEvent : Event
    {
        public string Email { get; }
        public string Username { get; }

        public ResetLinkGeneratedEvent(Guid aggregateId, string email, string username)
        {
            AggregateId = aggregateId;
            Email = email;
            Username = username;
        }
    }
}