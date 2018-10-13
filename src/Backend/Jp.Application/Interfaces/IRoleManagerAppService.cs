using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.RoleViewModels;

namespace Jp.Application.Interfaces
{
    public interface IRoleManagerAppService: IDisposable
    {
        Task<IEnumerable<RoleViewModel>> GetAllRoles();
        Task Remove(RemoveRoleViewModel model);
        Task<RoleViewModel> GetDetails(string name);
        Task Save(SaveRoleViewModel model);
        Task Update(UpdateRoleViewModel model);
        Task RemoveUserFromRole(RemoveUserFromRoleViewModel model);
    }
}
