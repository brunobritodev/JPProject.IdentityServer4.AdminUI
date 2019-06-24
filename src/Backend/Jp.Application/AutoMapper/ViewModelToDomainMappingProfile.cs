using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Application.ViewModels.RoleViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Client;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Commands.Role;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;

namespace Jp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            /*
             * Persisted grant
             */
            CreateMap<RemovePersistedGrantViewModel, RemovePersistedGrantCommand>().ConstructUsing(c => new RemovePersistedGrantCommand(c.Key));

            /*
             * User Creation Commands
             */
            CreateMap<RegisterUserViewModel, RegisterNewUserCommand>().ConstructUsing(c => new RegisterNewUserCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword));
            CreateMap<SocialViewModel, RegisterNewUserWithoutPassCommand>(MemberList.Source).ConstructUsing(c => new RegisterNewUserWithoutPassCommand(c.Email, c.Email, c.Name, c.Picture, c.Provider, c.ProviderId));
            CreateMap<RegisterUserViewModel, RegisterNewUserWithProviderCommand>().ConstructUsing(c => new RegisterNewUserWithProviderCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword, c.Picture, c.Provider, c.ProviderId));
            CreateMap<ForgotPasswordViewModel, SendResetLinkCommand>().ConstructUsing(c => new SendResetLinkCommand(c.UsernameOrEmail, c.UsernameOrEmail));
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>().ConstructUsing(c => new ResetPasswordCommand(c.Password, c.ConfirmPassword, c.Code, c.Email));
            CreateMap<ConfirmEmailViewModel, ConfirmEmailCommand>().ConstructUsing(c => new ConfirmEmailCommand(c.Code, c.Email));
            CreateMap<SocialViewModel, AddLoginCommand>().ConstructUsing(c => new AddLoginCommand(c.Email, c.Provider, c.ProviderId));

            /*
             * User Management commands
             */
            CreateMap<UserViewModel, UpdateProfileCommand>().ConstructUsing(c => new UpdateProfileCommand(c.Id, c.Url, c.Bio, c.Company, c.JobTitle, c.Name, c.PhoneNumber));
            CreateMap<UserViewModel, UpdateUserCommand>().ConstructUsing(c => new UpdateUserCommand(c.Email, c.UserName, c.Name, c.PhoneNumber, c.EmailConfirmed, c.PhoneNumberConfirmed, c.TwoFactorEnabled, c.LockoutEnd, c.LockoutEnabled, c.AccessFailedCount));
            CreateMap<ProfilePictureViewModel, UpdateProfilePictureCommand>().ConstructUsing(c => new UpdateProfilePictureCommand(c.Id, c.Picture));
            CreateMap<ChangePasswordViewModel, ChangePasswordCommand>().ConstructUsing(c => new ChangePasswordCommand(c.Id, c.OldPassword, c.NewPassword, c.ConfirmPassword));
            CreateMap<SetPasswordViewModel, SetPasswordCommand>().ConstructUsing(c => new SetPasswordCommand(c.Id, c.NewPassword, c.ConfirmPassword));
            CreateMap<RemoveAccountViewModel, RemoveAccountCommand>().ConstructUsing(c => new RemoveAccountCommand(c.Id));
            CreateMap<SaveUserClaimViewModel, SaveUserClaimCommand>().ConstructUsing(c => new SaveUserClaimCommand(c.Username, c.Type, c.Value));
            CreateMap<RemoveUserClaimViewModel, RemoveUserClaimCommand>().ConstructUsing(c => new RemoveUserClaimCommand(c.Username, c.Type));
            CreateMap<RemoveUserRoleViewModel, RemoveUserRoleCommand>().ConstructUsing(c => new RemoveUserRoleCommand(c.Username, c.Role));
            CreateMap<SaveUserRoleViewModel, SaveUserRoleCommand>().ConstructUsing(c => new SaveUserRoleCommand(c.Username, c.Role));
            CreateMap<RemoveUserLoginViewModel, RemoveUserLoginCommand>().ConstructUsing(c => new RemoveUserLoginCommand(c.Username, c.LoginProvider, c.ProviderKey));
            CreateMap<AdminChangePasswordViewodel, AdminChangePasswordCommand>().ConstructUsing(c => new AdminChangePasswordCommand(c.Password, c.ConfirmPassword, c.Username));

            /*
             * Client commands
             */
            CreateMap<ClientViewModel, UpdateClientCommand>().ConstructUsing(c => new UpdateClientCommand(c, c.OldClientId));
            CreateMap<RemoveClientSecretViewModel, RemoveClientSecretCommand>().ConstructUsing(c => new RemoveClientSecretCommand(c.Id, c.ClientId));
            CreateMap<RemovePropertyViewModel, RemovePropertyCommand>().ConstructUsing(c => new RemovePropertyCommand(c.Id, c.ClientId));
            CreateMap<SaveClientSecretViewModel, SaveClientSecretCommand>().ConstructUsing(c => new SaveClientSecretCommand(c.ClientId, c.Description, c.Value, c.Type, c.Expiration, (int)c.Hash.GetValueOrDefault(HashType.Sha256)));
            CreateMap<SaveClientPropertyViewModel, SaveClientPropertyCommand>().ConstructUsing(c => new SaveClientPropertyCommand(c.ClientId, c.Key, c.Value));
            CreateMap<SaveClientClaimViewModel, SaveClientClaimCommand>().ConstructUsing(c => new SaveClientClaimCommand(c.ClientId, c.Type, c.Value));
            CreateMap<RemoveClientClaimViewModel, RemoveClientClaimCommand>().ConstructUsing(c => new RemoveClientClaimCommand(c.Id, c.ClientId));
            CreateMap<RemoveClientViewModel, RemoveClientCommand>().ConstructUsing(c => new RemoveClientCommand(c.ClientId));
            CreateMap<CopyClientViewModel, CopyClientCommand>().ConstructUsing(c => new CopyClientCommand(c.ClientId));
            CreateMap<SaveClientViewModel, SaveClientCommand>().ConstructUsing(c => new SaveClientCommand(c.ClientId, c.ClientName, c.ClientUri, c.LogoUri, c.Description, c.ClientType));

            /*
             * Identity Resource commands
             */
            CreateMap<IdentityResource, RegisterIdentityResourceCommand>().ConstructUsing(c => new RegisterIdentityResourceCommand(c));
            CreateMap<IdentityResourceViewModel, UpdateIdentityResourceCommand>().ConstructUsing(c => new UpdateIdentityResourceCommand(c, c.OldName));
            CreateMap<RemoveIdentityResourceViewModel, RemoveIdentityResourceCommand>().ConstructUsing(c => new RemoveIdentityResourceCommand(c.Name));

            /*
             * Api Resource commands
             */
            CreateMap<ApiResource, RegisterApiResourceCommand>().ConstructUsing(c => new RegisterApiResourceCommand(c));
            CreateMap<UpdateApiResourceViewModel, UpdateApiResourceCommand>().ConstructUsing(c => new UpdateApiResourceCommand(c, c.OldApiResourceId));
            CreateMap<RemoveApiResourceViewModel, RemoveApiResourceCommand>().ConstructUsing(c => new RemoveApiResourceCommand(c.Name));

            CreateMap<SaveApiSecretViewModel, SaveApiSecretCommand>().ConstructUsing(c => new SaveApiSecretCommand(c.ResourceName, c.Description, c.Value, c.Type, c.Expiration, (int)c.Hash.GetValueOrDefault(HashType.Sha256)));
            CreateMap<RemoveApiSecretViewModel, RemoveApiSecretCommand>().ConstructUsing(c => new RemoveApiSecretCommand(c.Id, c.ResourceName));

            CreateMap<RemoveApiScopeViewModel, RemoveApiScopeCommand>().ConstructUsing(c => new RemoveApiScopeCommand(c.Id, c.ResourceName));
            CreateMap<SaveApiScopeViewModel, SaveApiScopeCommand>().ConstructUsing(c => new SaveApiScopeCommand(c.ResourceName, c.Name, c.Description, c.DisplayName, c.Emphasize, c.ShowInDiscoveryDocument, c.UserClaims));

            /*
             * Role commands
             */
            CreateMap<RemoveRoleViewModel, RemoveRoleCommand>().ConstructUsing(c => new RemoveRoleCommand(c.Name));
            CreateMap<SaveRoleViewModel, SaveRoleCommand>().ConstructUsing(c => new SaveRoleCommand(c.Name));
            CreateMap<UpdateRoleViewModel, UpdateRoleCommand>().ConstructUsing(c => new UpdateRoleCommand(c.Name, c.OldName));
            CreateMap<RemoveUserFromRoleViewModel, RemoveUserFromRoleCommand>().ConstructUsing(c => new RemoveUserFromRoleCommand(c.Role, c.Username));

        }
    }
}
