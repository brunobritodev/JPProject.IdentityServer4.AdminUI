using System;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{
    public class ApiResourceRemovedEvent : Event
    {
        public string Name { get; }

        public ApiResourceRemovedEvent(string name)
        {
            Name = name;
        }
    }
}