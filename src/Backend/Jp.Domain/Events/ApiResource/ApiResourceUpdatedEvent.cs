using System;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiResourceUpdatedEvent : Event
    {
        public IdentityServer4.Models.ApiResource ApiResource { get; }

        public ApiResourceUpdatedEvent(IdentityServer4.Models.ApiResource api)
        {
            ApiResource = api;
            AggregateId = api.Name;
        }
    }
}