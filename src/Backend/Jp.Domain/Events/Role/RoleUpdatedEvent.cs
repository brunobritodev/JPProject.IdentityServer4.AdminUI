using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class RoleUpdatedEvent : Event
    {
        public string Name { get; }
        public string OldName { get; }

        public RoleUpdatedEvent(string name, string oldName)
        {
            AggregateId = name;
            Name = name;
            OldName = oldName;
        }
    }
}
