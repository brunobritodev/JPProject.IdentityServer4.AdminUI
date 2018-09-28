using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Events.IdentityResource;
using MediatR;

namespace Jp.Domain.EventHandlers
{
    
    public class IdentityResourceEventHandler :
        INotificationHandler<IdentityResourceRegisteredEvent>
    {
        public Task Handle(IdentityResourceRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}