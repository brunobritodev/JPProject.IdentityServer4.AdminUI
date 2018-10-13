using Jp.Domain.Events.PersistedGrant;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.EventHandlers
{

    public class PersistedGrantEventHandler :
        INotificationHandler<PersistedGrantRemovedEvent>
    {
        public Task Handle(PersistedGrantRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}