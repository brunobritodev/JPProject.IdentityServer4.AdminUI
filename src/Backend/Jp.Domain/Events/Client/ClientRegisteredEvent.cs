using System;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientRegisteredEvent : Event
    {
        public RegisterClientCommand Request { get; }

        public ClientRegisteredEvent(RegisterClientCommand request)
        {
            Request = request;
        }
    }
}