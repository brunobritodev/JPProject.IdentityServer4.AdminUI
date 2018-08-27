using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class AccountPasswordReseted : Event
    {
        public string Email { get; }
        public string Code { get; }

        public AccountPasswordReseted(Guid aggregateId, string email, string code)
        {
            AggregateId = aggregateId;
            Email = email;
            Code = code;
        }
    }
}