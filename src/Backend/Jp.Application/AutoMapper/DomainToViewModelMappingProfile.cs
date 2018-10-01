using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Core.Events;
using Jp.Domain.Models;
using System.Globalization;
using Jp.Application.ViewModels.ApiResouceViewModels;

namespace Jp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
CreateMap<PersistedGrant, PersistedGrantViewModel>();
            CreateMap<ApiResource, ApiResourceListViewModel>();
            CreateMap<User, UserViewModel>().ForMember(a => a.Password, o => o.Ignore()).ForMember(a => a.ConfirmPassword, o => o.Ignore());
            CreateMap<StoredEvent, EventHistoryData>().ConstructUsing(a => new EventHistoryData() { Action = a.MessageType, Id = a.Id.ToString(), Details = a.Data, When = a.Timestamp.ToString(CultureInfo.InvariantCulture), Who = a.User });
            CreateMap<Client, ClientListViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.Secret, SecretViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientProperty, ClientPropertyViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientClaim, ClientClaimViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceListView>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ApiScope, ScopeViewModel>(MemberList.Destination);
        }
    }
}
