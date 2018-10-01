using System;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.PersistedGrant
{
    public class PersistedGrantUpdatedEvent : Event
    {
        public UpdatePersistedGrantCommand Request { get; }

        public PersistedGrantUpdatedEvent(Guid aggregateId, UpdatePersistedGrantCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}