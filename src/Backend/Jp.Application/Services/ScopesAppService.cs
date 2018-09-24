using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jp.Application.Interfaces;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class ScopesAppService : IScopesAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IIdentityResourcesRepository _identityResourcesRepository;
        private readonly IApiResourceRepository _apiResourceRepository;
        public IMediatorHandler Bus { get; set; }

        public ScopesAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IIdentityResourcesRepository identityResourcesRepository,
            IApiResourceRepository apiResourceRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _identityResourcesRepository = identityResourcesRepository;
            _apiResourceRepository = apiResourceRepository;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<string>> GetScopes(string search)
        {
            var identityScopes = await _identityResourcesRepository.GetScopes(search);
            var apiScopes = await _apiResourceRepository.GetScopes(search);
            identityScopes.AddRange(apiScopes);
            return identityScopes.OrderBy(a => a);
        }
    }
}