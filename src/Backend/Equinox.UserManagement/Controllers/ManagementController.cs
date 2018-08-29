using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.CrossCutting.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    [Route("[controller]"), Authorize]
    public class ManagementController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public ManagementController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IUserManager userManager,
            IMapper mapper) : base(notifications, mediator)
        {
            _userManager = userManager;
            this._mapper = mapper;
        }

        [Route("user-info"), HttpGet]
        public async Task<ActionResult<DefaultResponse<UserViewModel>>> UserInfo()
        {
            var user = await _userManager.GetUserAsync(GetUserId().Value);

            return Response(_mapper.Map<UserViewModel>(user));
        }

        [Route("ping"), HttpGet]
        public ActionResult<string> Ping() => Ok("pong");
    }

}