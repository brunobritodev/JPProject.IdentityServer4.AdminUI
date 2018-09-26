using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
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
        public IMediatorHandler Bus { get; set; }

        public ClientAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IClientRepository clientRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _clientRepository = clientRepository;
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

        public Task Update(Client client)
        {
            var registerCommand = _mapper.Map<UpdateClientCommand>(client);
            return Bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<SecretViewModel>> GetSecrets(string clientId)
        {
            return _mapper.Map<IEnumerable<SecretViewModel>>(await _clientRepository.GetSecrets(clientId));
        }

        public Task RemoveSecret(RemoveSecretViewModel model)
        {
            var registerCommand = _mapper.Map<RemoveSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task SaveSecret(SaveClientSecretViewModel model)
        {
            var registerCommand = _mapper.Map<SaveClientSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}