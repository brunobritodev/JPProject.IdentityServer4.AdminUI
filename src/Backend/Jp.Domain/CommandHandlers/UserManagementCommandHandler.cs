using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.UserManagement;
using Jp.Domain.Interfaces;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class UserManagementCommandHandler : CommandHandler,
        IRequestHandler<UpdateProfileCommand>,
        IRequestHandler<UpdateProfilePictureCommand>,
        IRequestHandler<SetPasswordCommand>,
        IRequestHandler<ChangePasswordCommand>,
        IRequestHandler<RemoveAccountCommand>
    {
        private readonly IUserService _userService;

        public UserManagementCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IUserService userService)
            : base(uow, bus, notifications)
        {
            _userService = userService;
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
    }
}
