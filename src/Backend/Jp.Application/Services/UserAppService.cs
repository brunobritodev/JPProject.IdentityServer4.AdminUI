using AutoMapper;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Commands.User;
using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Jp.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public UserAppService(IMapper mapper,
            IUserService userService,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _userService = userService;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public Task Register(RegisterUserViewModel model)
        {
            var registerCommand = _mapper.Map<RegisterNewUserCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task RegisterWithoutPassword(SocialViewModel model)
        {
            var registerCommand = _mapper.Map<RegisterNewUserWithoutPassCommand>(model);
            return Bus.SendCommand(registerCommand);
        }
        public Task RegisterWithProvider(RegisterUserViewModel model)
        {
            var registerCommand = _mapper.Map<RegisterNewUserWithProviderCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task SendResetLink(ForgotPasswordViewModel model)
        {
            var registerCommand = _mapper.Map<SendResetLinkCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task ResetPassword(ResetPasswordViewModel model)
        {
            var registerCommand = _mapper.Map<ResetPasswordCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task ConfirmEmail(ConfirmEmailViewModel model)
        {
            var registerCommand = _mapper.Map<ConfirmEmailCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public async Task<UserViewModel> FindByNameAsync(string username)
        {
            var user = await _userService.FindByNameAsync(username);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> FindByEmailAsync(string username)
        {
            var user = await _userService.FindByEmailAsync(username);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> FindByProviderAsync(string provider, string providerUserId)
        {
            var user = await _userService.FindByProviderAsync(provider, providerUserId);
            return _mapper.Map<UserViewModel>(user);
        }

        public Task AddLogin(SocialViewModel model)
        {
            var registerCommand = _mapper.Map<AddLoginCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task<bool> CheckUsername(string userName)
        {
            return _userService.UsernameExist(userName);
        }

        public Task<bool> CheckEmail(string email)
        {
            return _userService.EmailExist(email);
        }

        public async Task<RegisterUserViewModel> FindByLoginAsync(string provider, string providerUserId)
        {
            var model = await _userService.FindByLoginAsync(provider, providerUserId);
            return _mapper.Map<RegisterUserViewModel>(model);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
