using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Domain.Models;

namespace Jp.Domain.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task Remove(string name);
    }
}