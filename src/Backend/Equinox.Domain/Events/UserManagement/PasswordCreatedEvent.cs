using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.UserManagement
{
    public class PasswordCreatedEvent : Event
    {

        public PasswordCreatedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}