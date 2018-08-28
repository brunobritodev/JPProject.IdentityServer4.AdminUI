using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IUserService
    {
        Task<Guid?> CreateUserWithPass(IDomainUser user, string password);
        Task<Guid?> CreateUserWithProvider(IDomainUser user, string provider, string providerUserId);
        Task<Guid?> CreateUserWithProviderAndPass(IDomainUser user, string requestPassword, string requestProvider, string requestProviderId);
        Task<bool> UsernameExist(string userName);
        Task<bool> EmailExist(string email);
        Task<User> FindByLoginAsync(string provider, string providerUserId);
        Task<Guid?> SendResetLink(string requestEmail, string requestUsername);
        Task<Guid?> ResetPassword(string requestEmail, string requestPassword, string requestCode);
        Task<Guid?> ConfirmEmailAsync(string email, string code);
    }
}