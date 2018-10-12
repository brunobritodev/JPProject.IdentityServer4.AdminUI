using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels.RoleViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
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

        [HttpPost, Route("remove"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Remove([FromBody] RemoveRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _roleManagerAppService.Remove(model);
            return Response(true);
        }

        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<RoleViewModel>>> Details(string name)
        {
            var clients = await _roleManagerAppService.GetDetails(name);
            return Response(clients);
        }

        [HttpPost, Route("save"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> NewRole([FromBody] SaveRoleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _roleManagerAppService.Save(model);
            return Response(true);
        }

        [HttpPost, Route("update"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateRole([FromBody] UpdateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _roleManagerAppService.Update(model);
            return Response(true);
        }

        [HttpPost, Route("remove-user"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveUser([FromBody] RemoveUserFromRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _roleManagerAppService.RemoveUserFromRole(model);
            return Response(true);
        }
    }
}
