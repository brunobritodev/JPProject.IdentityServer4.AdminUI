using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Equinox.Domain.Commands.User;
using Equinox.Domain.Commands.UserManagement;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
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
    }
}