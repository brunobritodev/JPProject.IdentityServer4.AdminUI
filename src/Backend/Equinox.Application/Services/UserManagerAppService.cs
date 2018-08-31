using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands.UserManagement;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;


namespace Equinox.Application.Services
{
    public class UserManagerAppService : IUserManageAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IImageStorage _imageStorage;
        private readonly IMediatorHandler Bus;

        public UserManagerAppService(IMapper mapper,
            IUserService userService,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IImageStorage imageStorage
            )
        {
            _mapper = mapper;
            _userService = userService;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _imageStorage = imageStorage;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task UpdateProfile(ProfileViewModel model)
        {
            var registerCommand = _mapper.Map<UpdateProfileCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public async Task UpdateProfilePicture(ProfilePictureViewModel model)
        {
            var updateCommand = _mapper.Map<UpdateProfilePictureCommand>(model);
            updateCommand.Picture = await _imageStorage.SaveAsync(model);
            await Bus.SendCommand(updateCommand);
        }

        public Task CreatePassword(SetPasswordViewModel model)
        {
            var registerCommand = _mapper.Map<SetPasswordCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task RemoveAccount(RemoveAccountViewModel model)
        {
            var removeCommand = _mapper.Map<RemoveAccountCommand>(model);
            return Bus.SendCommand(removeCommand);
        }

        public Task ChangePassword(ChangePasswordViewModel model)
        {
            var registerCommand = _mapper.Map<ChangePasswordCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task<bool> HasPassword(Guid userId)
        {
            return _userService.HasPassword(userId);
        }

        public IEnumerable<EventHistoryData> GetHistoryLogs(Guid id)
        {
            var history = _mapper.Map<IEnumerable<EventHistoryData>>(_eventStoreRepository.All(id));
            return history;
        }
    }
}

