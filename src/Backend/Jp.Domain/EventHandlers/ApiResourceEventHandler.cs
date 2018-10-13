using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Events.ApiResource;
using MediatR;

namespace Jp.Domain.EventHandlers
{
    
    public class ApiResourceEventHandler :
        INotificationHandler<ApiResourceRegisteredEvent>
    {
        public Task Handle(ApiResourceRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}