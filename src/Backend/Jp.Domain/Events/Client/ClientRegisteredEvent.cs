using System;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientRemovedEvent : Event
    {
        public string ClientId { get; }

        public ClientRemovedEvent(string clientId)
        {
            ClientId = clientId;
        }
    }
}