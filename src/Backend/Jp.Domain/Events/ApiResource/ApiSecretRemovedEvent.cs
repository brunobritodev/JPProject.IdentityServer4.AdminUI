using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiSecretRemovedEvent : Event
    {
        public string ResourceName { get; }

        public ApiSecretRemovedEvent(int id, string resourceName)
        {
            AggregateId = id.ToString();
            ResourceName = resourceName;
        }
    }
}