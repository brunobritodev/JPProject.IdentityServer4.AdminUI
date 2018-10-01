using AutoMapper;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jp.Application.Services
{
    public class PersistedGrantAppService : IPersistedGrantAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IPersistedGrantRepository _persistedGrantRepository;
        public IMediatorHandler Bus { get; set; }

        public PersistedGrantAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IPersistedGrantRepository persistedGrantRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _persistedGrantRepository = persistedGrantRepository;
        }

        public async Task<IEnumerable<PersistedGrantViewModel>> GetPersistedGrants()
        {
            var resultado = await _persistedGrantRepository.GetGrants();
            return resultado.Select(s => _mapper.Map<PersistedGrantViewModel>(s));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}