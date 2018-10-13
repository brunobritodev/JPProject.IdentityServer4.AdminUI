using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserLoginRemovedEvent : Event
    {
        public string Username { get; }
        public string LoginProvider { get; }
        public string ProviderKey { get; }

        public UserLoginRemovedEvent(Guid aggregateId, string username, string loginProvider, string providerKey)
        {
            AggregateId = aggregateId.ToString();
            Username = username;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}