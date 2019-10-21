using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.UserManagement
{
    public class PasswordCreatedEvent : Event
    {

        public PasswordCreatedEvent(Guid aggregateId)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}