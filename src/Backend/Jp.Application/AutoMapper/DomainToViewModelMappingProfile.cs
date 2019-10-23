using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Core.Events;
using System.Globalization;
using System.Security.Claims;

namespace Jp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<IdentityServer4.EntityFramework.Entities.ApiResource, ApiResourceListViewModel>();

            CreateMap<StoredEvent, EventHistoryData>().ConstructUsing(a => new EventHistoryData(a.Message, a.Id.ToString(), a.Details, a.Timestamp.ToString(CultureInfo.InvariantCulture), a.User, a.MessageType, a.RemoteIpAddress));
            CreateMap<Client, ClientListViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.Secret, SecretViewModel>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientProperty, ClientPropertyViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.ClientClaim, ClaimViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.IdentityResource, IdentityResourceListView>(MemberList.Destination);
            CreateMap<IdentityServer4.EntityFramework.Entities.ApiScope, ScopeViewModel>();
            CreateMap<IdentityServer4.EntityFramework.Entities.UserClaim, ClaimViewModel>(MemberList.Destination);

            CreateMap<Claim, ClaimViewModel>(MemberList.Destination);

        }
    }
}
