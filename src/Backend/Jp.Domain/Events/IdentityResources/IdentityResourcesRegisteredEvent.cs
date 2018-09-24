using System;
using Jp.Domain.Commands.IdentityResources;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResources
{
    public class IdentityResourcesRegisteredEvent : Event
    {
        public RegisterIdentityResourcesCommand Request { get; }

        public IdentityResourcesRegisteredEvent(Guid aggregateId, RegisterIdentityResourcesCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}