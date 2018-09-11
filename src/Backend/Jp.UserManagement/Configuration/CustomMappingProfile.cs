using AutoMapper;
using Jp.Application.ViewModels;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;

namespace Jp.Management.Configuration
{
    internal class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {

            CreateMap<UserIdentity, ProfileViewModel>();
        }
    }
}