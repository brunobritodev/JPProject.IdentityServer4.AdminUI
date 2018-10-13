using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class RoleSavedEvent : Event
    {
        public RoleSavedEvent(string name)
        {
            AggregateId = name;
        }
    }
}