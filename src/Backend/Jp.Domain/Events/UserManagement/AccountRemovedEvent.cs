using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.UserManagement
{
    public class AccountRemovedEvent : Event
    {
        public AccountRemovedEvent(Guid aggregateId)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}