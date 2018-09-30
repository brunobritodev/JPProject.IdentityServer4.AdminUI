using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceRemovedEvent : Event
    {
        public string Name { get; }

        public IdentityResourceRemovedEvent(string name)
        {
            Name = name;
        }
    }
}