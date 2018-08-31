using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IUserManageAppService : IDisposable
    {
        Task UpdateProfile(ProfileViewModel model);
        Task UpdateProfilePicture(ProfilePictureViewModel model);
        Task ChangePassword(ChangePasswordViewModel model);
        Task CreatePassword(SetPasswordViewModel model);
        Task RemoveAccount(RemoveAccountViewModel model);
        Task<bool> HasPassword(Guid userId);
        IEnumerable<EventHistoryData> GetHistoryLogs(Guid value);
    }
}
