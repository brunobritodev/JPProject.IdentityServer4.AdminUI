using System;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientRemovedEvent : Event
    {
        public ClientRemovedEvent(string clientId)
        {
            AggregateId = clientId;
        }
    }
}