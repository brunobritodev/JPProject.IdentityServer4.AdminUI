using System;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientRegisteredEvent : Event
    {
        public RegisterClientCommand Request { get; }

        public ClientRegisteredEvent(Guid aggregateId, RegisterClientCommand request)
        {
            AggregateId = aggregateId;
            Request = request;
        }
    }
}