using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;

namespace Equinox.UserManagement.Configuration
{
    internal class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {

            CreateMap<UserIdentity, ProfileViewModel>();
        }
    }
}