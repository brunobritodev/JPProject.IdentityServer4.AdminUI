using System;

namespace Jp.Domain.Core.Events
{
    public class EventDetails
    {
        protected EventDetails() { }
        public EventDetails(Guid id, string metadata)
        {
            EventId = id;
            Metadata = metadata;
        }

        public Guid EventId { get; set; }
        public string Metadata { get; set; }
        public StoredEvent Event { get; set; }
    }
}