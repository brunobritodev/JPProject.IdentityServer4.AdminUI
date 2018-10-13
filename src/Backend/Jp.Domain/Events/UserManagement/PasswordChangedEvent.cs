using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.UserManagement
{
    public class PasswordChangedEvent : Event
    {

        public PasswordChangedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId.ToString();
        }
    }
}