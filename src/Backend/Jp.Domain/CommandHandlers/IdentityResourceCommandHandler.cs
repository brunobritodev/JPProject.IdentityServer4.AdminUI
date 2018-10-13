using IdentityServer4.EntityFramework.Mappers;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.IdentityResource;
using Jp.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.CommandHandlers
{
    public class IdentityResourceCommandHandler : CommandHandler,
        IRequestHandler<RegisterIdentityResourceCommand>,
        IRequestHandler<UpdateIdentityResourceCommand>,
        IRequestHandler<RemoveIdentityResourceCommand>
    {
        private readonly IIdentityResourceRepository _identityResourceRepository;

        public IdentityResourceCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IIdentityResourceRepository identityResourceRepository) : base(uow, bus, notifications)
        {
            _identityResourceRepository = identityResourceRepository;
        }


        public async Task Handle(RegisterIdentityResourceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _identityResourceRepository.GetByName(request.Resource.Name);
            if (savedClient != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Resource already exists"));
                return;
            }

            var irs = request.Resource.ToEntity();

            _identityResourceRepository.Add(irs);

            if (Commit())
            {
                await Bus.RaiseEvent(new IdentityResourceRegisteredEvent(irs.Name));
            }

        }

        public async Task Handle(UpdateIdentityResourceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _identityResourceRepository.GetByName(request.Resource.Name);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Resource not found"));
                return;
            }

            var irs = request.Resource.ToEntity();
            irs.Id = savedClient.Id;
            await _identityResourceRepository.UpdateWithChildrens(irs);

            if (Commit())
            {
                await Bus.RaiseEvent(new IdentityResourceUpdatedEvent(request.Resource));
            }
        }


        public async Task Handle(RemoveIdentityResourceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _identityResourceRepository.GetByName(request.Resource.Name);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Resource not found"));
                return;
            }

            _identityResourceRepository.Remove(savedClient.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new IdentityResourceRemovedEvent(request.Resource.Name));
            }
        }
    }
}