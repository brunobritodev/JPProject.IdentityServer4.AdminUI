using System;
using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.IdentityResources;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.IdentityResources;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class IdentityResourcesCommandHandler : CommandHandler,
        IRequestHandler<RegisterIdentityResourcesCommand>
    {
        private readonly IIdentityResourcesRepository _identityResourcesRepository;

        public IdentityResourcesCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IIdentityResourcesRepository identityResourcesRepository) : base(uow, bus, notifications)
        {
            _identityResourcesRepository = identityResourcesRepository;
        }


        public Task Handle(RegisterIdentityResourcesCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.CompletedTask;;
            }

			// Businness logic here
			
            //if (Commit())
            //{
            //    Bus.RaiseEvent(new IdentityResourcesRegisteredEvent(IdentityResource.Id));
            //}

            return Task.CompletedTask;
        }

    }
}