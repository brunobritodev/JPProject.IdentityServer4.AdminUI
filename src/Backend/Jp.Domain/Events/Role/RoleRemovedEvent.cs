using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class RoleRemovedEvent : Event
    {
        public RoleRemovedEvent(string name)
            : base(EventTypes.Success)
        {
            AggregateId = name;
        }
    }
}
