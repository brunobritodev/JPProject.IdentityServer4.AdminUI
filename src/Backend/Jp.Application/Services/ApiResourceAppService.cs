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
    public class ApiResourceAppService : IApiResourceAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IApiResourceRepository _apiResourceRepository;
        public IMediatorHandler Bus { get; set; }

        public ApiResourceAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IApiResourceRepository apiResourceRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _apiResourceRepository = apiResourceRepository;
        }


        public Task<IEnumerable<ApiResource>> GetApiResources()
        {
            var resultado = _apiResourceRepository.GetAll().Select(a => a.ToModel()).ToList();
            return Task.FromResult<IEnumerable<ApiResource>>(resultado);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}