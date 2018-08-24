using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Commands.User;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events.User;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using IdentityModel;
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand>,
        IRequestHandler<RegisterNewUserWithoutPassCommand>
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


        public async Task Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var user = new User()
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                Picture = request.Picture
            };

            var created = await _userService.CreateUser(user, request.Password);
            await _bus.RaiseEvent(new UserRegisteredeEvent(user.Id, user.Name, user.Email));
        }

        public async Task Handle(RegisterNewUserWithoutPassCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                Picture = request.Picture
            };

            await _userService.CreateUser(user, request.Provider, request.ProviderId);
        }

    }


}
