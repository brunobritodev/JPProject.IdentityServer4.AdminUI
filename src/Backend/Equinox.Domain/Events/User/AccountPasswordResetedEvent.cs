using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class AccountPasswordResetedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public AccountPasswordResetedEvent(Guid aggregateId, string email, string code)
        {
            AggregateId = aggregateId;
            Email = email;
            Code = code;
        }
    }
}