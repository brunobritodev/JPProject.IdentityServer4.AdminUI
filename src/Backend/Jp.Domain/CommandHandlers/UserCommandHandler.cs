using Jp.Domain.Commands.User;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.User;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand, bool>,
        IRequestHandler<RegisterNewUserWithoutPassCommand, bool>,
        IRequestHandler<RegisterNewUserWithProviderCommand, bool>,
        IRequestHandler<SendResetLinkCommand, bool>,
        IRequestHandler<ResetPasswordCommand, bool>,
        IRequestHandler<ConfirmEmailCommand, bool>,
        IRequestHandler<AddLoginCommand, bool>
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


        public async Task<bool> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = new User(
                id: Guid.NewGuid(),
                email: request.Email,
                name: request.Name,
                userName: request.Username,
                phoneNumber: request.PhoneNumber,
                picture: request.Picture
            );

            var emailAlreadyExist = await _userService.FindByEmailAsync(user.Email);
            if (emailAlreadyExist != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1001", "E-mail already exist. If you don't remember your passwork, reset it."));
                return false;
            }
            var usernameAlreadyExist = await _userService.FindByNameAsync(user.UserName);

            if (usernameAlreadyExist != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1002", "Username already exist. If you don't remember your passwork, reset it."));
                return false;
            }

            var id = await _userService.CreateUserWithPass(user, request.Password);
            if (id.HasValue)
            {
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RegisterNewUserWithoutPassCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var user = new User(
                id: Guid.NewGuid(),
                email: request.Email,
                name: request.Name,
                userName: request.Username,
                phoneNumber: request.PhoneNumber,
                picture: request.Picture
            );

            var emailAlreadyExist = await _userService.FindByEmailAsync(user.Email);
            if (emailAlreadyExist != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1001", "E-mail already exist. If you don't remember your passwork, reset it."));
                return false;
            }
            var usernameAlreadyExist = await _userService.FindByNameAsync(user.UserName);

            if (usernameAlreadyExist != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1002", "Username already exist. If you don't remember your passwork, reset it."));
                return false;
            }

            var id = await _userService.CreateUserWithProvider(user, request.Provider, request.ProviderId);
            if (id.HasValue)
            {
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RegisterNewUserWithProviderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var user = new User(
                id: Guid.NewGuid(),
                email: request.Email,
                name: request.Name,
                userName: request.Username,
                phoneNumber: request.PhoneNumber,
                picture: request.Picture);

            var id = await _userService.CreateUserWithProviderAndPass(user, request.Password, request.Provider, request.ProviderId);
            if (id.HasValue)
            {
                await Bus.RaiseEvent(new UserRegisteredEvent(id.Value, user.Name, user.Email));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SendResetLinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var emailSent = await _userService.SendResetLink(request.Email, request.Username);

            if (emailSent.HasValue)
            {
                await Bus.RaiseEvent(new ResetLinkGeneratedEvent(emailSent.Value, request.Email, request.Username));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var emailSent = await _userService.ResetPassword(request);

            if (emailSent.HasValue)
            {
                await Bus.RaiseEvent(new AccountPasswordResetedEvent(emailSent.Value, request.Email, request.Code));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var result = await _userService.ConfirmEmailAsync(request.Email, request.Code);
            if (result.HasValue)
            {
                await Bus.RaiseEvent(new EmailConfirmedEvent(request.Email, request.Code, result.Value));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(AddLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var result = await _userService.AddLoginAsync(request.Email, request.Provider, request.ProviderId);
            if (result.HasValue)
            {
                await Bus.RaiseEvent(new NewLoginAddedEvent(result.Value, request.Email, request.Provider, request.ProviderId));
                return true;
            }
            return false;
        }
    }


}
