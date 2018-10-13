using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.UserManagement
{
    public class PasswordCreatedEvent : Event
    {

        public PasswordCreatedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}