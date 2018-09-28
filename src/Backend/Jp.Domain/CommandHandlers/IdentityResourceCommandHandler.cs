using System;
using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.IdentityResource;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class IdentityResourceCommandHandler : CommandHandler,
        IRequestHandler<RegisterIdentityResourceCommand>
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


        public Task Handle(RegisterIdentityResourceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.CompletedTask;
            }

			// Businness logic here
			
            //if (Commit())
            //{
            //    Bus.RaiseEvent(new IdentityResourceRegisteredEvent(IdentityResource.Id));
            //}

            return Task.CompletedTask;
        }

    }
}