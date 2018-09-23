using System;
using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.Client;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class ClientCommandHandler : CommandHandler,
        IRequestHandler<RegisterClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IClientRepository clientRepository) : base(uow, bus, notifications)
        {
            _clientRepository = clientRepository;
        }


        public Task Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.CompletedTask;;
            }

			// Businness logic here
			
            //if (Commit())
            //{
            //    Bus.RaiseEvent(new ClientRegisteredEvent(Client.Id));
            //}

            return Task.CompletedTask;
        }

    }
}