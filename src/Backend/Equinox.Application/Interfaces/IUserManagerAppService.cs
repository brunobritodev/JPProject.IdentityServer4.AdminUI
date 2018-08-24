using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IUserManagerAppService : IDisposable
    {
        Task Register(UserViewModel model);
        Task<bool> CheckUsername(string userName);
        Task<bool> CheckEmail(string email);
        void PasswordSignInAsync(string modelUsername, string modelPassword, bool modelRememberLogin, bool lockoutOnFailure);
    }
}
