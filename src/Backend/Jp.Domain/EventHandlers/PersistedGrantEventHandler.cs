using Jp.Domain.Events.PersistedGrant;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.EventHandlers
{

    public class PersistedGrantEventHandler :
        INotificationHandler<PersistedGrantRegisteredEvent>
    {
        public Task Handle(PersistedGrantRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}