using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class IdentityResourcesAppService : IIdentityResourcesAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IIdentityResourcesRepository _identityResourcesRepository;
        public IMediatorHandler Bus { get; set; }

        public IdentityResourcesAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IIdentityResourcesRepository identityResourcesRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _identityResourcesRepository = identityResourcesRepository;
        }


        public Task<IEnumerable<IdentityResource>> GetIdentityResourcess()
        {
            var resultado = _identityResourcesRepository.GetAll().Select(id => id.ToModel()).ToList();
            return Task.FromResult<IEnumerable<IdentityResource>>(resultado);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}