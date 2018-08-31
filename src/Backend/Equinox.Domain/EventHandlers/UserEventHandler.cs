using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Events.User;
using MediatR;

namespace Equinox.Domain.EventHandlers
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
