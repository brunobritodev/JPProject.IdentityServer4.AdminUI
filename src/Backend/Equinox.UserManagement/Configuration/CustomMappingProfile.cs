using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;

namespace Equinox.UserManagement.Configuration
{
    internal class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {

            CreateMap<UserIdentity, UserViewModel>()
                .ForMember(c => c.Password, o => o.Ignore())
                .ForMember(c => c.ConfirmPassword, o => o.Ignore())
                .ForMember(c => c.Provider, o => o.Ignore())
                .ForMember(c => c.ProviderId, o => o.Ignore());
        }
    }
}