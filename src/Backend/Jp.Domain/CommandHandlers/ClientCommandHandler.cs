using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.Client;
using Jp.Domain.Interfaces;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class ClientCommandHandler : CommandHandler,
        IRequestHandler<RegisterClientCommand>,
        IRequestHandler<UpdateClientCommand>,
        IRequestHandler<RemoveSecretCommand>,
        IRequestHandler<SaveClientSecretCommand>,
        IRequestHandler<RemovePropertyCommand>,
        IRequestHandler<SaveClientPropertyCommand>,
        IRequestHandler<RemoveClientClaimCommand>,
        IRequestHandler<SaveClientClaimCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientSecretRepository _clientSecretRepository;
        private readonly IClientPropertyRepository _clientPropertyRepository;
        private readonly IClientClaimRepository _clientClaimRepository;

        public ClientCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IClientRepository clientRepository,
            IClientSecretRepository clientSecretRepository,
            IClientPropertyRepository clientPropertyRepository,
            IClientClaimRepository clientClaimRepository) : base(uow, bus, notifications)
        {
            _clientRepository = clientRepository;
            _clientSecretRepository = clientSecretRepository;
            _clientPropertyRepository = clientPropertyRepository;
            _clientClaimRepository = clientClaimRepository;
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
            var savedClient = await _clientRepository.GetClient(request.Client.ClientId);
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

        public async Task Handle(RemoveSecretCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            if (savedClient.ClientSecrets.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return;
            }

            _clientSecretRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientSecretRemovedEvent(request.Id, request.ClientId));
            }
        }

        public async Task Handle(SaveClientSecretCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            var secret = new ClientSecret
            {
                Client = savedClient,
                Description = request.Description,
                Expiration = request.Expiration,
                Type = request.Type,
                Value = request.GetValue()
            };

            _clientSecretRepository.Add(secret);

            if (Commit())
            {
                await Bus.RaiseEvent(new NewClientSecretEvent(request.Id, request.ClientId, secret.Type, secret.Description));
            }
        }

        public async Task Handle(RemovePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            if (savedClient.Properties.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return;
            }

            _clientPropertyRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientPropertyRemovedEvent(request.Id, request.ClientId));
            }
        }

        public async Task Handle(SaveClientPropertyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            var property = new ClientProperty()
            {
                Client = savedClient,
                Value = request.Value,
                Key = request.Key
            };

            _clientPropertyRepository.Add(property);

            if (Commit())
            {
                await Bus.RaiseEvent(new NewClientPropertyEvent(request.Id, request.ClientId, property.Key, property.Value));
            }
        }


        public async Task Handle(RemoveClientClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            if (savedClient.Claims.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return;
            }

            _clientClaimRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientClaimRemovedEvent(request.Id, request.ClientId));
            }
        }

        public async Task Handle(SaveClientClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }

            var property = new ClientClaim()
            {
                Client = savedClient,
                Value = request.Value,
                Type = request.Type
            };

            _clientClaimRepository.Add(property);

            if (Commit())
            {
                await Bus.RaiseEvent(new NewClientClaimEvent(request.Id, request.ClientId, property.Type, property.Value));
            }
        }
    }


}