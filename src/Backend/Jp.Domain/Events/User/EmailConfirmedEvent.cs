using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class EmailConfirmedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public EmailConfirmedEvent( string email, string code, Guid aggregateId)
        {
            Email = email;
            Code = code;
            AggregateId = aggregateId.ToString();
        }
    }
}
