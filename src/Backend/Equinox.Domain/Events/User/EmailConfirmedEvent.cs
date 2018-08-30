using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class EmailConfirmedEvent : Event
    {
        public string Email { get; }
        public string Code { get; }

        public EmailConfirmedEvent(string email, string code, Guid aggregateId)
        {
            Email = email;
            Code = code;
            AggregateId = aggregateId;
        }
    }
}
