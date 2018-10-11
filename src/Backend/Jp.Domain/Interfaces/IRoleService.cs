using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Domain.Models;

namespace Jp.Domain.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<bool> Remove(string name);
        Task<Role> Details(string name);
        Task<bool> Save(string name);
        Task<bool> Update(string name, string oldName);
    }
}