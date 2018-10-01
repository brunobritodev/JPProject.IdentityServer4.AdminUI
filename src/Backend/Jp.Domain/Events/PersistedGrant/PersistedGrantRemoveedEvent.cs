using System;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.PersistedGrant
{
    public class PersistedGrantRemovedEvent : Event
    {
        public RemovePersistedGrantCommand Request { get; }

        public PersistedGrantRemovedEvent(Guid aggregateId, RemovePersistedGrantCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}