using System;
using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.PersistedGrant;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class PersistedGrantCommandHandler : CommandHandler,
        IRequestHandler<RemovePersistedGrantCommand>
    {
        private readonly IPersistedGrantRepository _persistedGrantRepository;

        public PersistedGrantCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IPersistedGrantRepository persistedGrantRepository) : base(uow, bus, notifications)
        {
            _persistedGrantRepository = persistedGrantRepository;
        }


        public async Task Handle(RemovePersistedGrantCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

			// Businness logic here
			_persistedGrantRepository.Remove(request.Key);

            if (Commit())
            {
               await Bus.RaiseEvent(new PersistedGrantRemovedEvent(request.Key));
            }
        }

    }
}