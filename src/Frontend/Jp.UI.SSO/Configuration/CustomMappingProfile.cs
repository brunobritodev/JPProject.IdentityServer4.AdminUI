using AutoMapper;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;

namespace Jp.UI.SSO.Configuration
{
    internal class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {

            CreateMap<UserIdentity, UserViewModel>();
        }
    }
}