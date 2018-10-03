using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;

namespace Jp.Application.Services
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

        public Task UpdateProfile(UserViewModel model)
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

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return _mapper.Map<IEnumerable<UserListViewModel>>(users);
        }

        public async Task<UserViewModel> GetUserDetails(string username)
        {
            var users = await _userService.FindByNameAsync(username);
            return _mapper.Map<UserViewModel>(users);
        }

        public async Task<UserViewModel> GetUserAsync(Guid value)
        {
            var users = await _userService.GetUserAsync(value);
            return _mapper.Map<UserViewModel>(users);
        }

        public Task UpdateUser(UserViewModel model)
        {
            var command = _mapper.Map<UpdateUserCommand>(model);
            return Bus.SendCommand(command);
        }
    }
}

