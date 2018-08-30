using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands.User;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;

namespace Equinox.Application.Services
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

        public Task Register(UserViewModel model)
        {
            var registerCommand = _mapper.Map<RegisterNewUserCommand>(model);
            return Bus.SendCommand(registerCommand);
        }

        public Task RegisterWithoutPassword(SocialViewModel model)
        {
            var registerCommand = _mapper.Map<RegisterNewUserWithoutPassCommand>(model);
            return Bus.SendCommand(registerCommand);
        }
        public Task RegisterWithProvider(UserViewModel model)
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

        public Task<bool> CheckUsername(string userName)
        {
            return _userService.UsernameExist(userName);
        }

        public Task<bool> CheckEmail(string email)
        {
            return _userService.EmailExist(email);
        }

        public async Task<UserViewModel> FindByLoginAsync(string provider, string providerUserId)
        {
            var model = await _userService.FindByLoginAsync(provider, providerUserId);
            return _mapper.Map<UserViewModel>(model);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
