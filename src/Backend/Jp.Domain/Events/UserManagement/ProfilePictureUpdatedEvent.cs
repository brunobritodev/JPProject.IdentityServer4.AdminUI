using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.UserManagement
{
    public class ProfilePictureUpdatedEvent : Event
    {
        public string Picture { get; }

        public ProfilePictureUpdatedEvent(Guid aggregateId,string picture)
        {
            AggregateId = aggregateId.ToString();
            Picture = picture;
        }
    }
}