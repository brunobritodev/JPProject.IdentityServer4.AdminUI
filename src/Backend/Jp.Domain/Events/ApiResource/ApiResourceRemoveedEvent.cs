using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiResourceRemovedEvent : Event
    {
        public ApiResourceRemovedEvent(string name)
        {
            AggregateId = name;
        }
    }
}