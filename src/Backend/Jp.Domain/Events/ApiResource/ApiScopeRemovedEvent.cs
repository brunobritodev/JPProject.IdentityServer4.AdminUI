using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiScopeRemovedEvent : Event
    {
        public string ResourceName { get; }
        public string Name { get; }

        public ApiScopeRemovedEvent(int id, string resourceName, string name)
        {
            AggregateId = id.ToString();
            ResourceName = resourceName;
            Name = name;
        }
    }
}