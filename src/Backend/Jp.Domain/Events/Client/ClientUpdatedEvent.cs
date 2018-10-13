using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientUpdatedEvent : Event
    {
        public UpdateClientCommand Request { get; }

        public ClientUpdatedEvent(UpdateClientCommand request)
        {
            Request = request;
            AggregateId = request.Client.ClientId;
        }
    }
}