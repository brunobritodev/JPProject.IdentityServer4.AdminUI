using Jp.Application.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Application.Interfaces
{
    public interface IRoleManagerAppService : IDisposable
    {
        Task<IEnumerable<RoleViewModel>> GetAllRoles();
        Task Remove(RemoveRoleViewModel model);
        Task<RoleViewModel> GetDetails(string name);
        Task Save(SaveRoleViewModel model);
        Task Update(string id, UpdateRoleViewModel model);
        Task RemoveUserFromRole(RemoveUserFromRoleViewModel model);
    }
}
