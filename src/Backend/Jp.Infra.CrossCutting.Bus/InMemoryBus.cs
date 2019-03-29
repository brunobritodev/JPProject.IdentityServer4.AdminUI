using System.Threading.Tasks;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Commands;
using Jp.Domain.Core.Events;
using MediatR;

namespace Jp.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task<bool> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send<bool>(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}