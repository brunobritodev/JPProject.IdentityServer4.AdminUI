using Jp.Domain.Core.Events;

namespace Jp.Application.EventSourcedNormalizers
{
    public class EventHistoryData
    {
        public EventHistoryData(string action, string id, EventDetails details, string when, string who, string category, string ip)
        {
            Action = action;
            Id = id;
            When = when;
            Who = who;
            Category = category;
            Ip = ip;
            Details = details?.Metadata;
        }

        public string Category { get; }
        public string Ip { get; }
        public string Action { get; }
        public string Id { get; }
        public string When { get; }
        public string Who { get; }
        public string Details { get; }
    }
}