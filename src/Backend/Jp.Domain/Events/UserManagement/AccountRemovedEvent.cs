using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.UserManagement
{
    public class AccountRemovedEvent : Event
    {
        public AccountRemovedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}