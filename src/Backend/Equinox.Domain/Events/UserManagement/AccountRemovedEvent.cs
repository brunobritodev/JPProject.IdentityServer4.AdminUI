using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.UserManagement
{
    public class AccountRemovedEvent : Event
    {

        public AccountRemovedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}