using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, IDomainUser>(MemberList.Destination);
        }
    }
}
