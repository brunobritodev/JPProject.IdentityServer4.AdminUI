using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Domain.Commands.Client;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;

namespace Jp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            /*
             * User Creation Commands
             */
            CreateMap<UserViewModel, RegisterNewUserCommand>().ConstructUsing(c => new RegisterNewUserCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword));
            CreateMap<SocialViewModel, RegisterNewUserWithoutPassCommand>(MemberList.Source).ConstructUsing(c => new RegisterNewUserWithoutPassCommand(c.Email, c.Email, c.Name, c.Picture, c.Provider, c.ProviderId));
            CreateMap<UserViewModel, RegisterNewUserWithProviderCommand>().ConstructUsing(c => new RegisterNewUserWithProviderCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword, c.Picture, c.Provider, c.ProviderId));
            CreateMap<ForgotPasswordViewModel, SendResetLinkCommand>().ConstructUsing(c => new SendResetLinkCommand(c.UsernameOrEmail, c.UsernameOrEmail));
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>().ConstructUsing(c => new ResetPasswordCommand(c.Password, c.ConfirmPassword, c.Code, c.Email));
            CreateMap<ConfirmEmailViewModel, ConfirmEmailCommand>().ConstructUsing(c => new ConfirmEmailCommand(c.Code, c.Email));


            /*
             * User Management commands
             */
            CreateMap<ProfileViewModel, UpdateProfileCommand>().ConstructUsing(c => new UpdateProfileCommand(c.Id, c.Url, c.Bio, c.Company, c.JobTitle, c.Name, c.PhoneNumber));
            CreateMap<ProfilePictureViewModel, UpdateProfilePictureCommand>().ConstructUsing(c => new UpdateProfilePictureCommand(c.Id));

            CreateMap<ChangePasswordViewModel, ChangePasswordCommand>().ConstructUsing(c => new ChangePasswordCommand(c.Id, c.OldPassword, c.NewPassword, c.ConfirmPassword));
            CreateMap<SetPasswordViewModel, SetPasswordCommand>().ConstructUsing(c => new SetPasswordCommand(c.Id, c.NewPassword, c.ConfirmPassword));
            CreateMap<RemoveAccountViewModel, RemoveAccountCommand>().ConstructUsing(c => new RemoveAccountCommand(c.Id));

            /*
             * Client commands
             */
            CreateMap<Client, UpdateClientCommand>().ConstructUsing(c => new UpdateClientCommand(c));
            CreateMap<RemoveSecretViewModel, RemoveSecretCommand>().ConstructUsing(c => new RemoveSecretCommand(c.Id, c.ClientId));
            CreateMap<RemovePropertyViewModel, RemovePropertyCommand>().ConstructUsing(c => new RemovePropertyCommand(c.Id, c.ClientId));
            CreateMap<SaveClientSecretViewModel, SaveClientSecretCommand>().ConstructUsing(c => new SaveClientSecretCommand(c.ClientId, c.Description, c.Value, c.Type, c.Expiration, (int)c.Hash.GetValueOrDefault(HashType.Sha256)));
            CreateMap<SaveClientPropertyViewModel, SaveClientPropertyCommand>().ConstructUsing(c => new SaveClientPropertyCommand(c.ClientId, c.Key, c.Value));

            CreateMap<SaveClientClaimViewModel, SaveClientClaimCommand>().ConstructUsing(c => new SaveClientClaimCommand(c.ClientId, c.Type, c.Value));
            CreateMap<RemoveClientClaimViewModel, RemoveClientClaimCommand>().ConstructUsing(c => new RemoveClientClaimCommand(c.Id, c.ClientId));
        }
    }
}
