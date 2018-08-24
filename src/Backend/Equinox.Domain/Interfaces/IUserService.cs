using System.Collections.Generic;
using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;

namespace Equinox.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser<TUserId>(IUser<TUserId> user, string password);
        Task<bool> UsernameExist(string userName);
        Task<bool> EmailExist(string email);
    }
}