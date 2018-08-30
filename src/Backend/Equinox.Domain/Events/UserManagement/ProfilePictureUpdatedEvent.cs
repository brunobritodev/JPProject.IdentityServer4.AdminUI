using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.UserManagement
{
    public class ProfilePictureUpdatedEvent : Event
    {
        public string Picture { get; }

        public ProfilePictureUpdatedEvent(Guid aggregateId,string picture)
        {
            AggregateId = aggregateId;
            Picture = picture;
        }
    }
}