using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Commands.User;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events.User;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUserService _userService;

        public UserCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IUserService userService) : base(uow, bus, notifications)
        {
            _bus = bus;
            _userService = userService;
        }


        public async Task Handle(RegisterNewUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var user = new User()
            {
                Email = message.Email,
                Name = message.Name,
                UserName = message.Username,
                PhoneNumber = message.PhoneNumber,
            };

            var created = await _userService.CreateUser(user, message.Password);
            await _bus.RaiseEvent(new UserRegisteredeEvent(user.Id, user.Name, user.Email));
        }
    }


}
