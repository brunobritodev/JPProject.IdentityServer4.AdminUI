using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceRemovedEvent : Event
    {
        public IdentityResourceRemovedEvent(string name)
        {
            AggregateId = name;
        }
    }
}