using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserWithPass(IDomainUser user, string password);
        Task<bool> CreateUserWithProvider(IDomainUser user, string provider, string providerUserId);
        Task<bool> CreateUserWithProviderAndPass(IDomainUser user, string requestPassword, string requestProvider, string requestProviderId);
        Task<bool> UsernameExist(string userName);
        Task<bool> EmailExist(string email);
        Task<User> FindByLoginAsync(string provider, string providerUserId);
        }
}