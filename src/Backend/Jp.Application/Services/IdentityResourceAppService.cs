using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class IdentityResourceAppService : IIdentityResourceAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IIdentityResourceRepository _identityResourceRepository;
        public IMediatorHandler Bus { get; set; }

        public IdentityResourceAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IIdentityResourceRepository identityResourceRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _identityResourceRepository = identityResourceRepository;
        }


        public Task<IEnumerable<IdentityResource>> GetIdentityResources()
        {
            var resultado = _identityResourceRepository.GetAll().Select(s => s.ToModel()).ToList();
            return Task.FromResult<IEnumerable<IdentityResource>>(resultado);
        }

        public async Task<IdentityResource> GetDetails(string name)
        {
            var irs = await _identityResourceRepository.GetByName(name);
            return irs.ToModel();
        }

        public Task Save(IdentityResource model)
        {
            var command = _mapper.Map<RegisterIdentityResourceCommand>(model);
            return Bus.SendCommand(command);
        }

        public Task Update(IdentityResource model)
        {
            var command = _mapper.Map<UpdateIdentityResourceCommand>(model);
            return Bus.SendCommand(command);
        }

        public Task Remove(RemoveIdentityResourceViewModel model)
        {
            var command = _mapper.Map<RemoveIdentityResourceCommand>(model);
            return Bus.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}