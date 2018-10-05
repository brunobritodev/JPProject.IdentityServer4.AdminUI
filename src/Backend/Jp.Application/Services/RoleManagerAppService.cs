using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;

namespace Jp.Application.Services
{
    public class RoleManagerAppService : IRoleManagerAppService
    {
        private IEventStoreRepository _eventStoreRepository;
        private IMapper _mapper;
        private readonly IRoleService _roleService;

        public IMediatorHandler Bus { get; set; }
        public RoleManagerAppService(IMapper mapper,
            IRoleService roleService,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository
        )
        {
            _mapper = mapper;
            _roleService = roleService;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            return _mapper.Map<IEnumerable<RoleViewModel>>(await _roleService.GetAllRoles());
        }
    }
}