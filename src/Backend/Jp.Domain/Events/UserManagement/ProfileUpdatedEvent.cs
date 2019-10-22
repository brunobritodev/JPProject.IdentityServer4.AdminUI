using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.UserManagement
{
    public class ProfileUpdatedEvent : Event
    {
        public UpdateProfileCommand Request { get; }

        public ProfileUpdatedEvent(Guid aggregateId, UpdateProfileCommand request)
            : base(EventTypes.Success)
        {
            AggregateId = aggregateId.ToString();
            Request = request;
        }
    }
}
