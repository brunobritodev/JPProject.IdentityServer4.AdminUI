using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiSecretRemovedEvent : Event
    {
        public int Id { get; }
        public string ResourceName { get; }

        public ApiSecretRemovedEvent(int id, string resourceName)
        {
            Id = id;
            ResourceName = resourceName;
        }
    }
}