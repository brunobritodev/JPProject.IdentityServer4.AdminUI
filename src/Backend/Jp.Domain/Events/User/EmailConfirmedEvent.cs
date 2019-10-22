using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class EmailConfirmedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public EmailConfirmedEvent(string email, string code, Guid aggregateId)
            : base(EventTypes.Success)
        {
            Email = email;
            Code = code;
            AggregateId = aggregateId.ToString();
        }
    }
}
