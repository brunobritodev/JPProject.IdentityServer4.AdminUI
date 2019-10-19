using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserLoggedOutEvent : Event
    {
        public UserLoggedOutEvent(string aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}