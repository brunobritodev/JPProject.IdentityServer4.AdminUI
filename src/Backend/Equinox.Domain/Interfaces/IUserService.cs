using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(IDomainUser user, string password);
        Task<bool> UsernameExist(string userName);
        Task<bool> EmailExist(string email);
        Task<User> FindByLoginAsync(string provider, string providerUserId);
        Task<bool> CreateUser(IDomainUser user, string provider, string providerUserId);
    }
}