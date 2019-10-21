using System;

namespace Jp.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(
            string aggregateId,
            string messageType,
            EventTypes eventType,
            string customMessage,
            string localIpAddress,
            string remoteIpAddress,
            string data) : base(eventType)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            MessageType = messageType;
            EventType = eventType;
            Message = customMessage;
            LocalIpAddress = localIpAddress;
            RemoteIpAddress = remoteIpAddress;
            Data = data;
        }

        public StoredEvent SetUser(string user)
        {
            this.User = user;
            return this;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
        public StoredEvent ReplaceTimeStamp(in DateTime timeStamp)
        {
            Timestamp = timeStamp;
            return this;
        }
    }
}