using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class AccountPasswordResetedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public AccountPasswordResetedEvent(Guid aggregateId, string email, string code)
        {
            AggregateId = aggregateId.ToString();
            Email = email;
            Code = code;
        }
    }
}