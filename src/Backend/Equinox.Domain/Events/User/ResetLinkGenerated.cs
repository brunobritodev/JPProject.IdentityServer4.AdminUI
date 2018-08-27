using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class ResetLinkGenerated : Event
    {
        public string Email { get; }
        public string Username { get; }

        public ResetLinkGenerated(Guid aggregateId, string email, string username)
        {
            AggregateId = aggregateId;
            Email = email;
            Username = username;
        }
    }
}