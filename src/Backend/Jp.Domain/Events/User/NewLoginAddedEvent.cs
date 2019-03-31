using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class NewLoginAddedEvent : Event
    {
        public string Email { get; }
        public string Provider { get; }
        public string ProviderId { get; }

        public NewLoginAddedEvent(Guid aggregateId, string email, string provider, string providerId)
        {
            Email = email;
            Provider = provider;
            ProviderId = providerId;
            AggregateId = aggregateId.ToString();
        }
    }
}