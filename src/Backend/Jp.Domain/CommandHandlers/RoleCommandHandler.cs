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
        IRequestHandler<RemoveRoleCommand>
    {
        private readonly IRoleService _roleService;

        public RoleCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IRoleService roleService) : base(uow, bus, notifications)
        {
            _roleService = roleService;
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
    }
}
