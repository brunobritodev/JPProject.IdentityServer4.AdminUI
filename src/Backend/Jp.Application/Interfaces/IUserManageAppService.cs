using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.RoleViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.ViewModels;
using Jp.Domain.Models;

namespace Jp.Application.Interfaces
{
    public interface IUserManageAppService : IDisposable
    {
        Task UpdateProfile(UserViewModel model);
        Task UpdateProfilePicture(ProfilePictureViewModel model);
        Task ChangePassword(ChangePasswordViewModel model);
        Task CreatePassword(SetPasswordViewModel model);
        Task RemoveAccount(RemoveAccountViewModel model);
        Task<bool> HasPassword(Guid userId);
        Task<IEnumerable<EventHistoryData>> GetHistoryLogs(string username);
        
        Task<UserViewModel> GetUserDetails(string username);
        Task<UserViewModel> GetUserAsync(Guid value);
        Task UpdateUser(UserViewModel model);

        Task<IEnumerable<ClaimViewModel>> GetClaims(string userName);
        Task SaveClaim(SaveUserClaimViewModel model);
        Task RemoveClaim(RemoveUserClaimViewModel model);
        Task<IEnumerable<RoleViewModel>> GetRoles(string userName);
        Task RemoveRole(RemoveUserRoleViewModel model);
        Task SaveRole(SaveUserRoleViewModel model);
        Task<IEnumerable<UserLoginViewModel>> GetLogins(string userName);
        Task RemoveLogin(RemoveUserLoginViewModel model);
        Task<IEnumerable<UserListViewModel>> GetUsersInRole(string[] role);
        Task ResetPassword(AdminChangePasswordViewodel model);
        Task<ListOfUsersViewModel> GetUsers(PagingViewModel page);
    }
}
