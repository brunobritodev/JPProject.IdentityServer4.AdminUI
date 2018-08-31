using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Domain.Commands.UserManagement;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.UserManagement
{
    public class ProfileUpdatedEvent : Event
    {
        public UpdateProfileCommand Request { get; }

        public ProfileUpdatedEvent(Guid aggregateId,UpdateProfileCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}
