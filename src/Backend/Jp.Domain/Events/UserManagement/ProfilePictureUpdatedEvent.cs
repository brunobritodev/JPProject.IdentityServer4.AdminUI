using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.UserManagement
{
    public class ProfilePictureUpdatedEvent : Event
    {
        public string Picture { get; }

        public ProfilePictureUpdatedEvent(Guid aggregateId, string picture)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Picture = picture;
        }
    }
}