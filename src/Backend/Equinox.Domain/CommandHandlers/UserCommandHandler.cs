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
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand>,
        IRequestHandler<RegisterNewUserWithoutPassCommand>,
        IRequestHandler<RegisterNewUserWithProviderCommand>,
        IRequestHandler<SendResetLinkCommand>,
        IRequestHandler<ResetPasswordCommand>,
        IRequestHandler<ConfirmEmailCommand>
    {
        private readonly IUserService _userService;

        public UserCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IUserService userService) : base(uow, bus, notifications)
        {
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

            var id = await _userService.CreateUserWithPass(user, request.Password);
            if (id.HasValue)
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
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

            var id = await _userService.CreateUserWithProvider(user, request.Provider, request.ProviderId);
            if (id.HasValue)
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
        }

        public async Task Handle(RegisterNewUserWithProviderCommand request, CancellationToken cancellationToken)
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
            var id = await _userService.CreateUserWithProviderAndPass(user, request.Password, request.Provider, request.ProviderId);
            if (id.HasValue)
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
        }

        public async Task Handle(SendResetLinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var emailSent = await _userService.SendResetLink(request.Email, request.Username);

            if (emailSent.HasValue)
                await Bus.RaiseEvent(new ResetLinkGeneratedEvent(emailSent.Value, request.Email, request.Username));
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var emailSent = await _userService.ResetPassword(request);

            if (emailSent.HasValue)
                await Bus.RaiseEvent(new AccountPasswordResetedEvent(emailSent.Value, request.Email, request.Code));
        }

        public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.ConfirmEmailAsync(request.Email, request.Code);
            if (result.HasValue)
                await Bus.RaiseEvent(new EmailConfirmedEvent(request.Email, request.Code, result.Value));
        }
    }


}
