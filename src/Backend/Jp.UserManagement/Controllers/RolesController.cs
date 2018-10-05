using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]")]
    public class RolesController : ApiController
    {
        private readonly IRoleManagerAppService _roleManagerAppService;

        public RolesController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IRoleManagerAppService roleManagerAppService) : base(notifications, mediator)
        {
            _roleManagerAppService = roleManagerAppService;
        }

        [HttpGet, Route("all-roles")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<RoleViewModel>>>> AllRoles()
        {
            var clients = await _roleManagerAppService.GetAllRoles();
            return Response(clients);
        }
    }
}
