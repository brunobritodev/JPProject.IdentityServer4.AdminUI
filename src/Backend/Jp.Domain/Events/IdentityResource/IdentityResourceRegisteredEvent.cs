using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceRegisteredEvent : Event
    {
        public IdentityResourceRegisteredEvent(string name)
            : base(EventTypes.Success)
        {
            AggregateId = name;
        }
    }
}