using MediatR;
using System;

namespace Jp.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        /// <summary>
        /// Gets or sets the time stamp when the event was raised.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime Timestamp { get; protected set; }

        public EventTypes EventType { get; set; }
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

        /// <summary>
        /// Gets or sets the event message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        protected Event() { }
        protected Event(EventTypes eventType)
        {
            Timestamp = DateTime.Now;
        }
    }
}