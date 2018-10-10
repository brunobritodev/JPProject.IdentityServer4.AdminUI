using Jp.Domain.Commands.Role;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.Role;
using Jp.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.CommandHandlers
{
    public class RoleCommandHandler : CommandHandler,
        IRequestHandler<RemoveRoleCommand>,
        IRequestHandler<SaveRoleCommand>,
        IRequestHandler<UpdateRoleCommand>,
        IRequestHandler<RemoveUserFromRoleCommand> 
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IRoleService roleService,
            IUserService userService) : base(uow, bus, notifications)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public async Task Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            // Businness logic here
            await _roleService.Remove(request.Name);

            if (Commit())
            {
                await Bus.RaiseEvent(new RoleRemovedEvent(request.Name));
            }
        }

        public async Task Handle(SaveRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            // Businness logic here
            var result = await _roleService.Save(request.Name);

            if (result)
            {
                await Bus.RaiseEvent(new RoleSavedEvent(request.Name));
            }
        }

        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            // Businness logic here
            var result = await _roleService.Update(request.Name, request.OldName);

            if (result)
            {
                await Bus.RaiseEvent(new RoleUpdatedEvent(request.Name, request.OldName));
            }
        }

        public async Task Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            // Businness logic here
            var result = await _userService.RemoveUserFromRole(request.Name, request.Username);

            if (result)
            {
                await Bus.RaiseEvent(new UserRemovedFromRoleEvent(request.Name, request.Username));
            }
        }
    }
}
