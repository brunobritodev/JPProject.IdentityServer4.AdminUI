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
        INotificationHandler<UserRegisteredeEvent>
    {
        public Task Handle(UserRegisteredeEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
