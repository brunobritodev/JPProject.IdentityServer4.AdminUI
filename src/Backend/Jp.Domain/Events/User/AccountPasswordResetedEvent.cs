using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class AccountPasswordResetedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public AccountPasswordResetedEvent(Guid aggregateId, string email, string code)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Email = email;
            Code = code;
        }
    }
}