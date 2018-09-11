using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class ClientAppService : IClientAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IClientRepository _ClientRepository;
        public IMediatorHandler Bus { get; set; }

        public ClientAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IClientRepository clientRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _ClientRepository = clientRepository;
        }

        public Task<IEnumerable<Client>> GetClients()
        {

            var resultado = _ClientRepository.GetAll().Select(a => a.ToModel()).ToList();
            return Task.FromResult<IEnumerable<Client>>(resultado);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}