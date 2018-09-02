using System;
using System.Threading.Tasks;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;

namespace Jp.Infra.CrossCutting.Identity.Services
{
    public interface IUserManager
    {
        Task<UserIdentity> FindByEmailAsync(string email);
        Task<UserIdentity> FindByNameAsync(string username);
        Task<UserIdentity> FindByProviderAsync(string provider, string providerUserId);
        Task<UserIdentity> GetUserAsync(Guid user);
    }
}