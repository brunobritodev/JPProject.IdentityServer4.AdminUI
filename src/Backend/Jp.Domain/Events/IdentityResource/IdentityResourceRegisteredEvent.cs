using System;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.IdentityResource
{
    public class IdentityResourceRegisteredEvent : Event
    {
        public string Name { get; }

        public IdentityResourceRegisteredEvent(string name)
        {
            Name = name;
        }
    }
}