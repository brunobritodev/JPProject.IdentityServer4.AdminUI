using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Domain.Commands.Client;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class ClientAppService : IClientAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClientPropertyRepository _clientPropertyRepository;
        private readonly IClientSecretRepository _clientSecretRepository;
        private readonly IClientClaimRepository _clientClaimRepository;
        public IMediatorHandler Bus { get; set; }

        public ClientAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IClientRepository clientRepository,
            IClientPropertyRepository clientPropertyRepository,
            IClientSecretRepository clientSecretRepository,
            IClientClaimRepository clientClaimRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _clientRepository = clientRepository;
            _clientPropertyRepository = clientPropertyRepository;
            _clientSecretRepository = clientSecretRepository;
            _clientClaimRepository = clientClaimRepository;
        }

        public Task<IEnumerable<ClientListViewModel>> GetClients()
        {
            var resultado = _mapper.Map<IEnumerable<ClientListViewModel>>(_clientRepository.GetAll().Select(a => a.ToModel()).OrderBy(a => a.ClientName).ToList());
            return Task.FromResult(resultado);
        }

        public async Task<Client> GetClientDetails(string clientId)
        {
            var resultado = await _clientRepository.GetClient(clientId);
            return _mapper.Map<Client>(resultado);
        }

        public Task Update(ClientViewModel client)
        {
            var registerCommand = _mapper.Map<UpdateClientCommand>(client);
            return Bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<SecretViewModel>> GetSecrets(string clientId)
        {
            return _mapper.Map<IEnumerable<SecretViewModel>>(await _clientSecretRepository.GetByClientId(clientId));
        }

        public Task RemoveSecret(RemoveClientSecretViewModel model)
        {
            var registerCommand = _mapper.Map<RemoveClientSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task SaveSecret(SaveClientSecretViewModel model)
        {
            var registerCommand = _mapper.Map<SaveClientSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<ClientPropertyViewModel>> GetProperties(string clientId)
        {
            return _mapper.Map<IEnumerable<ClientPropertyViewModel>>(await _clientPropertyRepository.GetByClientId(clientId));
        }

        public Task RemoveProperty(RemovePropertyViewModel model)
        {
            var registerCommand = _mapper.Map<RemovePropertyCommand>(model);
            return Bus.SendCommand(registerCommand);
    }
        public Task SaveProperty(SaveClientPropertyViewModel model)
        {
            var registerCommand = _mapper.Map<SaveClientPropertyCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<ClaimViewModel>> GetClaims(string clientId)
        {
            return _mapper.Map<IEnumerable<ClaimViewModel>>(await _clientClaimRepository.GetByClientId(clientId));
        }

        public Task RemoveClaim(RemoveClientClaimViewModel model)
        {
            var registerCommand = _mapper.Map<RemoveClientClaimCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task SaveClaim(SaveClientClaimViewModel model)
        {
            var registerCommand = _mapper.Map<SaveClientClaimCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task Save(SaveClientViewModel client)
        {
            var command = _mapper.Map<SaveClientCommand>(client);
            return Bus.SendCommand(command);
        }

        public Task Remove(RemoveClientViewModel client)
        {
            var command = _mapper.Map<RemoveClientCommand>(client);
            return Bus.SendCommand(command);
        }

        public Task Copy(CopyClientViewModel client)
        {
            var command = _mapper.Map<CopyClientCommand>(client);
            return Bus.SendCommand(command);
        }

        public async Task<Client> GetClientDefaultDetails(string clientId)
        {
            var resultado = await _clientRepository.GetClientDefaultDetails(clientId);
            return _mapper.Map<Client>(resultado);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}