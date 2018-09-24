using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Mappers;
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
        IRequestHandler<RegisterClientCommand>,
        IRequestHandler<UpdateClientCommand>
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
                return Task.CompletedTask; ;
            }

            // Businness logic here

            //if (Commit())
            //{
            //    Bus.RaiseEvent(new ClientRegisteredEvent(Client.Id));
            //}

            return Task.CompletedTask;
        }

        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return; 
            }

            // Businness logic here
            var savedClient = await _clientRepository.GetByUniqueName(request.Client.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            var client = request.Client.ToEntity();
            client.Id = savedClient.Id;
            await _clientRepository.UpdateWithChildrens(client);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientUpdatedEvent(request));
            }

        }
    }
}