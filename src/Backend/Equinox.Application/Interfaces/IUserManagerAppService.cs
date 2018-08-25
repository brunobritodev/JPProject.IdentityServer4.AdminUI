using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Equinox.Application.ViewModels;
using Equinox.Domain.Interfaces;

namespace Equinox.Application.Interfaces
{
    public interface IUserManagerAppService : IDisposable
    {
        Task Register(UserViewModel model);
        Task RegisterWithoutPassword(SocialViewModel model);
        Task<bool> CheckUsername(string userName);
        Task<bool> CheckEmail(string email);
        Task<UserViewModel> FindByLoginAsync(string provider, string providerUserId);
    }
}
