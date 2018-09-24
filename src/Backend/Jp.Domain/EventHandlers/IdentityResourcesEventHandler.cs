using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Events.IdentityResources;
using MediatR;

namespace Jp.Domain.EventHandlers
{
    
    public class IdentityResourcesEventHandler :
        INotificationHandler<IdentityResourcesRegisteredEvent>
    {
        public Task Handle(IdentityResourcesRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}