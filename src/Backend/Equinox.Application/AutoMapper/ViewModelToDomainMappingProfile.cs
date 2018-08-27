using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Commands.User;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>().ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>().ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));

            CreateMap<UserViewModel, RegisterNewUserCommand>().ConstructUsing(c => new RegisterNewUserCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword));
            CreateMap<SocialViewModel, RegisterNewUserWithoutPassCommand>(MemberList.Source).ConstructUsing(c => new RegisterNewUserWithoutPassCommand(c.Email, c.Email, c.Name, c.Picture, c.Provider, c.ProviderId));
            CreateMap<UserViewModel, RegisterNewUserWithProviderCommand>().ConstructUsing(c => new RegisterNewUserWithProviderCommand(c.Username, c.Email, c.Name, c.PhoneNumber, c.Password, c.ConfirmPassword, c.Picture, c.Provider, c.ProviderId));
            CreateMap<ForgotPasswordViewModel, SendResetLinkCommand>().ConstructUsing(c => new SendResetLinkCommand(c.UsernameOrEmail, c.UsernameOrEmail));
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>().ConstructUsing(c => new ResetPasswordCommand(c.Password, c.ConfirmPassword, c.Code, c.Email));

            CreateMap<ConfirmEmailViewModel, ConfirmEmailCommand>().ConstructUsing(c => new ConfirmEmailCommand(c.Code, c.Email));
        }
    }
}
