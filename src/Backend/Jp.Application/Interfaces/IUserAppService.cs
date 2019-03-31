using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using System;
using System.Threading.Tasks;

namespace Jp.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        Task Register(RegisterUserViewModel model);
        Task RegisterWithoutPassword(SocialViewModel model);
        Task<bool> CheckUsername(string userName);
        Task<bool> CheckEmail(string email);
        Task<RegisterUserViewModel> FindByLoginAsync(string provider, string providerUserId);
        Task RegisterWithProvider(RegisterUserViewModel model);
        Task SendResetLink(ForgotPasswordViewModel model);
        Task ResetPassword(ResetPasswordViewModel model);
        Task ConfirmEmail(ConfirmEmailViewModel model);
        Task<UserViewModel> FindByNameAsync(string username);
        Task<UserViewModel> FindByEmailAsync(string username);
        Task<UserViewModel> FindByProviderAsync(string provider, string providerUserId);
        Task AddLogin(SocialViewModel user);
    }
}
