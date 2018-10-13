using System;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiResourceRegisteredEvent : Event
    {
        public ApiResourceRegisteredEvent(string name)
        {
            AggregateId = name;
        }
    }
}