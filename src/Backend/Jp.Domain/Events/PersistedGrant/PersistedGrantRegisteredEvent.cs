using System;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.PersistedGrant
{
    public class PersistedGrantRemovedEvent : Event
    {
        public string Key { get; }

        public PersistedGrantRemovedEvent(string key)
        {
            Key = key;
            AggregateId = key;
        }
    }
}