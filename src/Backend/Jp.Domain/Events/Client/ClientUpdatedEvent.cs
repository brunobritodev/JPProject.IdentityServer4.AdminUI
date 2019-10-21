using Jp.Domain.Commands.Clients;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.Client
{
    public class ClientUpdatedEvent : Event
    {
        public UpdateClientCommand Request { get; }

        public ClientUpdatedEvent(UpdateClientCommand request)
            : base(EventTypes.Success)
        {
            Request = request;
            AggregateId = request.Client.ClientId;
        }
    }
}