using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Events.User;
using MediatR;

namespace Jp.Domain.EventHandlers
{
    
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<EmailConfirmedEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(EmailConfirmedEvent notification, CancellationToken cancellationToken)
        {
            // Send some e-mail. Alert admin
            return Task.CompletedTask;
        }
    }
}
