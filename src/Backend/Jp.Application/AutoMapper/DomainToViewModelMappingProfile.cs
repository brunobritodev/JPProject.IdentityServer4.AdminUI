using System.Globalization;
using AutoMapper;
using IdentityServer4.Models;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Events;
using Jp.Domain.Models;

namespace Jp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(a => a.Password, o => o.Ignore()).ForMember(a => a.ConfirmPassword, o => o.Ignore());
            CreateMap<StoredEvent, EventHistoryData>().ConstructUsing(a => new EventHistoryData() { Action = a.MessageType, Id = a.Id.ToString(), Details = a.Data, When = a.Timestamp.ToString(CultureInfo.InvariantCulture), Who = a.User});

        }
    }
}
