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
        IRequestHandler<UpdateProfileCommand>,
        IRequestHandler<UpdateProfilePictureCommand>,
        IRequestHandler<SetPasswordCommand>,
        IRequestHandler<ChangePasswordCommand>,
        IRequestHandler<RemoveAccountCommand>,
        IRequestHandler<UpdateUserCommand>,
        IRequestHandler<SaveUserClaimCommand>,
        IRequestHandler<RemoveUserClaimCommand>,
        IRequestHandler<RemoveUserRoleCommand>,
        IRequestHandler<SaveUserRoleCommand>
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

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.UpdateProfileAsync(request);
            if (result)
                await Bus.RaiseEvent(new ProfileUpdatedEvent(request.Id.Value, request));
        }

        public async Task Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.UpdateProfilePictureAsync(request);
            if (result)
                await Bus.RaiseEvent(new ProfilePictureUpdatedEvent(request.Id.Value, request.Picture));
        }

        public async Task Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.CreatePasswordAsync(request);
            if (result)
                await Bus.RaiseEvent(new PasswordCreatedEvent(request.Id.Value));
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.ChangePasswordAsync(request);
            if (result)
                await Bus.RaiseEvent(new PasswordChangedEvent(request.Id.Value));
        }

        public async Task Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var result = await _userService.RemoveAccountAsync(request);
            if (result)
                await Bus.RaiseEvent(new AccountRemovedEvent(request.Id.Value));
        }


        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }
            user.Email = request.Email;
            user.EmailConfirmed = request.EmailConfirmed;
            user.AccessFailedCount = request.AccessFailedCount;
            user.LockoutEnabled = request.LockoutEnabled;
            user.LockoutEnd = request.LockoutEnd;
            user.Name = request.Name;
            user.TwoFactorEnabled = request.TwoFactorEnabled;
            user.PhoneNumber = request.PhoneNumber;
            user.PhoneNumberConfirmed = request.PhoneNumberConfirmed;
            await _userService.UpdateUserAsync(user);
        }

        public async Task Handle(SaveUserClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }

            var claim = new Claim(request.Type, request.Value);

            var success = await _userService.SaveClaim(userDb.Id, claim);

            if (success)
            {
                await Bus.RaiseEvent(new NewUserClaimEvent(request.Username, request.Type, request.Value));
            }
        }

        public async Task Handle(RemoveUserClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }

            var success = await _userService.RemoveClaim(userDb.Id, request.Type);

            if (success)
            {
                await Bus.RaiseEvent(new UserClaimRemovedEvent(request.Username, request.Type));
            }
        }

        public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var userDb = await _userService.FindByNameAsync(request.Username);
            if (userDb == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }

            var success = await _userService.RemoveRole(userDb.Id, request.Role);

            if (success)
            {
                await Bus.RaiseEvent(new UserRoleRemovedEvent(_user.UserId, request.Username, request.Role));
            }
        }

        public async Task Handle(SaveUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }

            var success = await _userService.SaveRole(user.Id, request.Role);

            if (success)
            {
                await Bus.RaiseEvent(new UserRoleSavedEvent(_user.UserId, request.Username, request.Role));
            }
        }

        public async Task Handle(RemoveUserLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var user = await _userService.FindByNameAsync(request.Username);
            if (user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "User not found"));
                return;
            }

            var success = await _userService.RemoveLogin(user.Id, request.LoginProvider, request.ProviderKey);

            if (success)
            {
                await Bus.RaiseEvent(new UserLoginRemovedEvent(_user.UserId, request.Username, request.LoginProvider, request.ProviderKey));
            }
        }
    }
}
