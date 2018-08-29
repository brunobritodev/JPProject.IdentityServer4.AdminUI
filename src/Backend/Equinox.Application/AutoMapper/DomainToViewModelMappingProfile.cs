using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Models;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<User, UserViewModel>().ForMember(a => a.Password, o => o.Ignore()).ForMember(a => a.ConfirmPassword, o => o.Ignore());
        }
    }
}
