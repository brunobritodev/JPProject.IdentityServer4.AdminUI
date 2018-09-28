using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientClonedEvent : Event
    {
        public string From { get; }
        public string To { get; }

        public ClientClonedEvent(string from, string to)
        {
            From = @from;
            To = to;
        }
    }
}