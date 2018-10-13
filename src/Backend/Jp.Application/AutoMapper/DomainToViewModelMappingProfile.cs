using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Events;
using Jp.Domain.Models;
using System.Globalization;
using System.Security.Claims;
using Jp.Application.ViewModels.RoleViewModels;

namespace Jp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ApiResource, ApiResourceListViewModel>();
            CreateMap<User, UserViewModel>(MemberList.Destination);//.ForMember(x => x.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd != null ? src.LockoutEnd.Value.DateTime.ToShortDateString() : string.Empty));
            CreateMap<User, UserListViewModel>(MemberList.Destination);

            CreateMap<StoredEvent, EventHistoryData>().ConstructUsing(a => new EventHistoryData() { Action = a.MessageType, Id = a.Id.ToString(), Details = a.Data, When = a.Timestamp.ToString(CultureInfo.InvariantCulture), Who = a.User });
            CreateMap<Client, ClientListViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.Secret, SecretViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientProperty, ClientPropertyViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientClaim, ClaimViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceListView>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ApiScope, ScopeViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.UserClaim, ClaimViewModel>(MemberList.Destination);

            CreateMap<Claim, ClaimViewModel>(MemberList.Destination);
            CreateMap<Role, RoleViewModel>(MemberList.Destination);
            CreateMap<UserLogin, UserLoginViewModel>(MemberList.Destination);
            
        }
    }
}
