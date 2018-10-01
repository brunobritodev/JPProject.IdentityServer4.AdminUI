using System;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.PersistedGrant
{
    public class PersistedGrantRegisteredEvent : Event
    {
        public RegisterPersistedGrantCommand Request { get; }

        public PersistedGrantRegisteredEvent(Guid aggregateId, RegisterPersistedGrantCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}