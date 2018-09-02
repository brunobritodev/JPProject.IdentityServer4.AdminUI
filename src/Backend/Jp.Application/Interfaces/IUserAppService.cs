using System;
using System.Threading.Tasks;
using Jp.Application.ViewModels;

namespace Jp.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        Task Register(UserViewModel model);
        Task RegisterWithoutPassword(SocialViewModel model);
        Task<bool> CheckUsername(string userName);
        Task<bool> CheckEmail(string email);
        Task<UserViewModel> FindByLoginAsync(string provider, string providerUserId);
        Task RegisterWithProvider(UserViewModel model);
        Task SendResetLink(ForgotPasswordViewModel model);
        Task ResetPassword(ResetPasswordViewModel model);
        Task ConfirmEmail(ConfirmEmailViewModel model);
    }
}
