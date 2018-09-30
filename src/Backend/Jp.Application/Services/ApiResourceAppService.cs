using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;

namespace Jp.Application.Services
{
    public class ApiResourceAppService : IApiResourceAppService
    {
        private IMapper _mapper;
        private IEventStoreRepository _eventStoreRepository;
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiSecretRepository _secretRepository;
        public IMediatorHandler Bus { get; set; }

        public ApiResourceAppService(IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IApiResourceRepository apiResourceRepository,
            IApiSecretRepository secretRepository)
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _apiResourceRepository = apiResourceRepository;
            _secretRepository = secretRepository;
        }

        public Task<IEnumerable<ApiResourceListViewModel>> GetApiResources()
        {
            var resultado = _apiResourceRepository.GetAll().Select(s => _mapper.Map<ApiResourceListViewModel>(s)).ToList();
            return Task.FromResult<IEnumerable<ApiResourceListViewModel>>(resultado);
        }

        public async Task<ApiResource> GetDetails(string name)
        {
            var resultado = await _apiResourceRepository.GetByName(name);
            return resultado.ToModel();
        }

        public Task Save(ApiResource model)
        {
            var command = _mapper.Map<RegisterApiResourceCommand>(model);
            return Bus.SendCommand(command);
        }

        public Task Update(ApiResource model)
        {
            var command = _mapper.Map<UpdateApiResourceCommand>(model);
            return Bus.SendCommand(command);

        }

        public Task Remove(RemoveApiResourceViewModel model)
        {
            var command = _mapper.Map<RemoveApiResourceCommand>(model);
            return Bus.SendCommand(command);
        }

        public async Task<IEnumerable<SecretViewModel>> GetSecrets(string name)
        {
            return _mapper.Map<IEnumerable<SecretViewModel>>(await _secretRepository.GetByApiName(name));
        }

        public Task RemoveSecret(RemoveApiSecretViewModel model)
        {
            var registerCommand = _mapper.Map<RemoveApiSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task SaveSecret(SaveApiSecretViewModel model)
        {
            var registerCommand = _mapper.Map<SaveApiSecretCommand>(model);
            return Bus.SendCommand(registerCommand);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}