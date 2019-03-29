using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.User;
using Jp.Domain.Events.UserManagement;
using Jp.Domain.Interfaces;
using MediatR;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.CommandHandlers
{
    public class UserManagementCommandHandler : CommandHandler,
        IRequestHandler<UpdateProfileCommand, bool>,
        IRequestHandler<UpdateProfilePictureCommand, bool>,
        IRequestHandler<SetPasswordCommand, bool>,
        IRequestHandler<ChangePasswordCommand, bool>,
        IRequestHandler<RemoveAccountCommand, bool>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<SaveUserClaimCommand, bool>,
        IRequestHandler<RemoveUserClaimCommand, bool>,
        IRequestHandler<RemoveUserRoleCommand, bool>,
        IRequestHandler<SaveUserRoleCommand, bool>,
        IRequestHandler<AdminChangePasswordCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly ISystemUser _user;

        public UserManagementCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IUserService userService,
            ISystemUser user)
            : base(uow, bus, notifications)
        {
            _userService = userService;
            _user = user;
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _userService.UpdateProfileAsync(request);
            if (result)
            {
                await Bus.RaiseEvent(new ProfileUpdatedEvent(request.Id.Value, request));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _userService.UpdateProfilePictureAsync(request);
            if (result)
            {
                await Bus.RaiseEvent(new ProfilePictureUpdatedEvent(request.Id.Value, request.Picture));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _userService.CreatePasswordAsync(request);
            if (result)
            {
                await Bus.RaiseEvent(new PasswordCreatedEvent(request.Id.Value));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _userService.ChangePasswordAsync(request);
            if (result)
            {
                await Bus.RaiseEvent(new PasswordChangedEvent(request.Id.Value));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _userService.RemoveAccountAsync(request);
            if (result)
            {
                await Bus.RaiseEvent(new AccountRemovedEvent(request.Id.Value));
                return true;
            }
            return false;
        }


        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }
            user.UpdateInfo(request);
            await _userService.UpdateUserAsync(user);

            return true;
        }

        public async Task<bool> Handle(SaveUserClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var claim = new Claim(request.Type, request.Value);

            var success = await _userService.SaveClaim(userDb.Id, claim);

            if (success)
            {
                await Bus.RaiseEvent(new NewUserClaimEvent(request.Username, request.Type, request.Value));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveUserClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var success = await _userService.RemoveClaim(userDb.Id, request.Type);

            if (success)
            {
                await Bus.RaiseEvent(new UserClaimRemovedEvent(request.Username, request.Type));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var success = await _userService.RemoveRole(userDb.Id, request.Role);

            if (success)
            {
                await Bus.RaiseEvent(new UserRoleRemovedEvent(_user.UserId, request.Username, request.Role));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SaveUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var success = await _userService.SaveRole(user.Id, request.Role);

            if (success)
            {
                await Bus.RaiseEvent(new UserRoleSavedEvent(_user.UserId, request.Username, request.Role));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveUserLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var success = await _userService.RemoveLogin(user.Id, request.LoginProvider, request.ProviderKey);

            if (success)
            {
                await Bus.RaiseEvent(new UserLoginRemovedEvent(_user.UserId, request.Username, request.LoginProvider, request.ProviderKey));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(AdminChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return false;
            }

            var success = await _userService.ResetPasswordAsync(request.Username, request.Password);

            if (success)
            {
                await Bus.RaiseEvent(new AdminChangedPasswordEvent(user.Id, request.Username));
                return true;
            }
            return false;
        }
    }
}
