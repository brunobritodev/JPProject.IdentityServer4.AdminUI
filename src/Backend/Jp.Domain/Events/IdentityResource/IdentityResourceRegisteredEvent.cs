using System;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceRegisteredEvent : Event
    {
        public RegisterIdentityResourceCommand Request { get; }

        public IdentityResourceRegisteredEvent(Guid aggregateId, RegisterIdentityResourceCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}