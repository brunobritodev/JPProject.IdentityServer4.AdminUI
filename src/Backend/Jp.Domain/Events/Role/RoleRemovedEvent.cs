using System;
using System.Collections.Generic;
using System.Text;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Role
{
    public class RoleRemovedEvent : Event
    {
        public RoleRemovedEvent(string name)
        {
            AggregateId = name;
        }
    }
}
