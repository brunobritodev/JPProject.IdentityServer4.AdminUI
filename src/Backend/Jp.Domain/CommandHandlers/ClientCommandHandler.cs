using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Events.Client;
using Jp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jp.Domain.CommandHandlers
{
    public class ClientCommandHandler : CommandHandler,
        IRequestHandler<RemoveClientCommand, bool>,
        IRequestHandler<UpdateClientCommand, bool>,
        IRequestHandler<RemoveClientSecretCommand, bool>,
        IRequestHandler<SaveClientSecretCommand, bool>,
        IRequestHandler<RemovePropertyCommand, bool>,
        IRequestHandler<SaveClientPropertyCommand, bool>,
        IRequestHandler<RemoveClientClaimCommand, bool>,
        IRequestHandler<SaveClientClaimCommand, bool>,
        IRequestHandler<SaveClientCommand, bool>,
        IRequestHandler<CopyClientCommand, bool>
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


        public async Task<bool> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false; ;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }
            _clientRepository.Remove(savedClient.Id);
            if (Commit())
            {
                await Bus.RaiseEvent(new ClientRemovedEvent(request.Client.ClientId));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetClient(request.OldClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }

            var client = request.Client.ToEntity();
            client.Id = savedClient.Id;
            await _clientRepository.UpdateWithChildrens(client);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientUpdatedEvent(request));
                return true;
            }
            return false;

        }

        public async Task<bool> Handle(RemoveClientSecretCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }

            if (savedClient.ClientSecrets.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return false;
            }

            _clientSecretRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientSecretRemovedEvent(request.Id, request.ClientId));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SaveClientSecretCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
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
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemovePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }

            if (savedClient.Properties.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return false;
            }

            _clientPropertyRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientPropertyRemovedEvent(request.Id, request.ClientId));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SaveClientPropertyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
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
                return true;
            }
            return false;
        }


        public async Task<bool> Handle(RemoveClientClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetClient(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }

            if (savedClient.Claims.All(f => f.Id != request.Id))
            {
                await Bus.RaiseEvent(new DomainNotification("2", "Invalid secret"));
                return false;
            }

            _clientClaimRepository.Remove(request.Id);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientClaimRemovedEvent(request.Id, request.ClientId));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SaveClientClaimCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetByClientId(request.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
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
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(SaveClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client already exists"));
                return false;
            }

            var client = request.ToEntity();

            _clientRepository.Add(client);

            if (Commit())
            {
                await Bus.RaiseEvent(new NewClientEvent(request.Client.ClientId, request.ClientType, request.Client.ClientName));
                return true;
            }

            return false;
        }



        public async Task<bool> Handle(CopyClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return false;
            }

            var copyOf = savedClient.ToModel();
            copyOf.ClientId = $"copy-of-{copyOf.ClientId}-{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
            copyOf.ClientSecrets = new List<IdentityServer4.Models.Secret>();
            copyOf.ClientName = "Copy of " + copyOf.ClientName;
            var newClient = copyOf.ToEntity();

            _clientRepository.Add(newClient);

            if (Commit())
            {
                await Bus.RaiseEvent(new ClientClonedEvent(request.Client.ClientId, newClient.ClientId));
                return true;
            }

            return false;
        }
    }


}