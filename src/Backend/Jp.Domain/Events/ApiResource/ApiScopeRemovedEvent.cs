using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiScopeRemovedEvent : Event
    {
        public int Id { get; }
        public string ResourceName { get; }
        public string Name { get; }

        public ApiScopeRemovedEvent(int id, string resourceName, string name)
        {
            Id = id;
            ResourceName = resourceName;
            Name = name;
        }
    }
}