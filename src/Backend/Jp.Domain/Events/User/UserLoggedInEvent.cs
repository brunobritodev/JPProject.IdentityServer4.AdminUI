using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class UserLoggedInEvent : Event
    {
        public UserLoggedInEvent(string subjectId, string provider)
        {
            AggregateId = subjectId;
            Provider = provider;
            FederationGateway = true;
        }
        public bool FederationGateway { get; set; }
        public string Provider { get; set; }
    }
}
