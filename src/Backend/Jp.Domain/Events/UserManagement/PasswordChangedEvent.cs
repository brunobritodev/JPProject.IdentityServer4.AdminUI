using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.UserManagement
{
    public class PasswordChangedEvent : Event
    {

        public PasswordChangedEvent(Guid aggregateId)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}