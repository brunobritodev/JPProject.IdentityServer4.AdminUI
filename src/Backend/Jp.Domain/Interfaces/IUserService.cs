using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Models;

namespace Jp.Domain.Interfaces
{
    public interface IUserService
    {
        Task<Guid?> CreateUserWithPass(IDomainUser user, string password);
        Task<Guid?> CreateUserWithProvider(IDomainUser user, string provider, string providerUserId);
        Task<Guid?> CreateUserWithProviderAndPass(IDomainUser user, string password, string provider, string providerId);
        Task<bool> UsernameExist(string userName);
        Task<bool> EmailExist(string email);
        Task<User> FindByLoginAsync(string provider, string providerUserId);
        Task<Guid?> SendResetLink(string email, string username);
        Task<Guid?> ResetPassword(ResetPasswordCommand request);
        Task<Guid?> ConfirmEmailAsync(string email, string code);
        Task<bool> UpdateProfileAsync(UpdateProfileCommand command);
        Task<bool> UpdateProfilePictureAsync(UpdateProfilePictureCommand command);
        Task<bool> CreatePasswordAsync(SetPasswordCommand request);
        Task<bool> ChangePasswordAsync(ChangePasswordCommand request);
        Task<bool> RemoveAccountAsync(RemoveAccountCommand request);
        Task<bool> HasPassword(Guid userId);

        Task<IEnumerable<IDomainUser>> GetByIdAsync(params string[] id);
    }
}