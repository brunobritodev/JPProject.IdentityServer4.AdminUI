using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceUpdatedEvent : Event
    {
        public IdentityServer4.Models.IdentityResource Resource { get; }

        public IdentityResourceUpdatedEvent(IdentityServer4.Models.IdentityResource resource)
        {
            Resource = resource;
            AggregateId = resource.Name;
        }
    }
}