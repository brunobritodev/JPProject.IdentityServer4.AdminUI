using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class RoleSavedEvent : Event
    {
        public string Name { get; }

        public RoleSavedEvent(string name)
        {
            Name = name;
        }
    }
}