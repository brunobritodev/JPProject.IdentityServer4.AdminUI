using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
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
        IRequestHandler<RemoveClientCommand>,
        IRequestHandler<UpdateClientCommand>,
        IRequestHandler<RemoveClientSecretCommand>,
        IRequestHandler<SaveClientSecretCommand>,
        IRequestHandler<RemovePropertyCommand>,
        IRequestHandler<SaveClientPropertyCommand>,
        IRequestHandler<RemoveClientClaimCommand>,
        IRequestHandler<SaveClientClaimCommand>,
        IRequestHandler<SaveClientCommand>,
        IRequestHandler<CopyClientCommand>
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


        public async Task Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return; ;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
            }
            _clientRepository.Remove(savedClient.Id);
            if (Commit())
            {
                await Bus.RaiseEvent(new ClientRemovedEvent(request.Client.ClientId));
            }
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

        public async Task Handle(RemoveClientSecretCommand request, CancellationToken cancellationToken)
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

        public async Task Handle(SaveClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient != null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client already exists"));
                return;
            }

            PrepareClientTypeForNewClient(request);
            var client = request.Client.ToEntity();
            client.Description = request.Description;

            _clientRepository.Add(client);

            if (Commit())
            {
                await Bus.RaiseEvent(new NewClientEvent(request.Client.ClientId, request.ClientType, request.Client.ClientName));
            }
        }

        private void PrepareClientTypeForNewClient(SaveClientCommand command)
        {
            switch (command.ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.WebImplicit:
                    command.Client.AllowedGrantTypes = GrantTypes.Implicit;
                    command.Client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.WebHybrid:
                    command.Client.AllowedGrantTypes = GrantTypes.Hybrid;
                    break;
                case ClientType.Spa:
                    command.Client.AllowedGrantTypes = GrantTypes.Implicit;
                    command.Client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.Native:
                    command.Client.AllowedGrantTypes = GrantTypes.Hybrid;
                    break;
                case ClientType.Machine:
                    command.Client.AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task Handle(CopyClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return;
            }

            var savedClient = await _clientRepository.GetByClientId(request.Client.ClientId);
            if (savedClient == null)
            {
                await Bus.RaiseEvent(new DomainNotification("1", "Client not found"));
                return;
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
            }
        }
    }


}