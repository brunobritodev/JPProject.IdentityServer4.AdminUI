using System;

namespace Jp.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(
            string messageType,
            EventTypes eventType,
            string customMessage,
            string localIpAddress,
            string remoteIpAddress,
            string data) : base(eventType)
        {
            Id = Guid.NewGuid();
            MessageType = messageType;
            EventType = eventType;
            Message = customMessage;
            LocalIpAddress = localIpAddress;
            RemoteIpAddress = remoteIpAddress;
            Details = new EventDetails(Id, data);
        }

        public StoredEvent SetUser(string user)
        {
            this.User = user;
            return this;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; private set; }
        /// <summary>
        /// Gets or sets the local ip address of the current request.
        /// </summary>
        /// <value>
        /// The local ip address.
        /// </value>
        public string LocalIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the remote ip address of the current request.
        /// </summary>
        /// <value>
        /// The remote ip address.
        /// </value>
        public string RemoteIpAddress { get; set; }

        public string User { get; private set; }
        public EventDetails Details { get; set; }

        public StoredEvent ReplaceTimeStamp(in DateTime timeStamp)
        {
            Timestamp = timeStamp;
            return this;
        }

        public StoredEvent SetAggregate(string aggregateId)
        {
            AggregateId = aggregateId;
            return this;
        }
    }

}