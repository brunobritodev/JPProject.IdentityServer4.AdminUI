using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.UserManagement
{
    public class PasswordChangedEvent : Event
    {

        public PasswordChangedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}