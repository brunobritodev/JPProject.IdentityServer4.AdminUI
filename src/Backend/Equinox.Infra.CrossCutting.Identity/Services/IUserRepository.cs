using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;

namespace Equinox.Infra.CrossCutting.Identity.Services
{
    public interface IUserManager
    {
        Task<UserIdentity> FindByEmailAsync(string email);
        Task<UserIdentity> FindByNameAsync(string username);
        Task<UserIdentity> FindByProviderAsync(string provider, string providerUserId);
        Task<UserIdentity> GetUserAsync(Guid user);
    }
}