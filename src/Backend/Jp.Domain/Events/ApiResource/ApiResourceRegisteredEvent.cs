using System;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiResourceRegisteredEvent : Event
    {
        public RegisterApiResourceCommand Request { get; }

        public ApiResourceRegisteredEvent(Guid aggregateId, RegisterApiResourceCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}