using System.Globalization;
using AutoMapper;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Events;
using Equinox.Domain.Models;

namespace Equinox.Application.AutoMapper
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
