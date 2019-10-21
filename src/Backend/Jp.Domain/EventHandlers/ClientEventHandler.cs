using Jp.Domain.Events.Client;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.EventHandlers
{

    public class ClientEventHandler :
        INotificationHandler<ClientRemovedEvent>,
        INotificationHandler<ClientUpdatedEvent>
    {
        public Task Handle(ClientRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}