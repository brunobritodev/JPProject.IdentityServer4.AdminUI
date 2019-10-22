using Jp.Application.Interfaces;
using Jp.Application.ViewModels.RoleViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("roles"), Authorize(Policy = "ReadOnly")]
    public class RolesController : ApiController
    {
        private readonly IRoleManagerAppService _roleManagerAppService;
        private readonly IUserManageAppService _userManageAppService;

        public RolesController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IRoleManagerAppService roleManagerAppService,
            IUserManageAppService userManageAppService) : base(notifications, mediator)
        {
            _roleManagerAppService = roleManagerAppService;
            _userManageAppService = userManageAppService;
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> List()
        {
            var clients = await _roleManagerAppService.GetAllRoles();
            return ResponseGet(clients);
        }

        [HttpGet, Route("{role}")]
        public async Task<ActionResult<RoleViewModel>> Details(string role)
        {
            var clients = await _roleManagerAppService.GetDetails(role);
            return ResponseGet(clients);
        }

        [HttpDelete, Route("{role}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Remove(string role)
        {
            var model = new RemoveRoleViewModel(role);
            await _roleManagerAppService.Remove(model);
            return ResponseDelete();
        }

        [HttpPost, Route(""), Authorize(Policy = "Admin")]
        public async Task<ActionResult<RoleViewModel>> NewRole([FromBody] SaveRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _roleManagerAppService.Save(model);
            var savedModel = await _roleManagerAppService.GetDetails(model.Name);

            return ResponsePost(nameof(Details), new { role = savedModel.Name }, savedModel);
        }

        [HttpPut, Route("{role}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> UpdateRole(string role, [FromBody] UpdateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _roleManagerAppService.Update(role, model);
            return ResponsePutPatch();
        }

        [HttpGet, Route("{role}/users")]
        public async Task<ActionResult<IEnumerable<UserListViewModel>>> UsersFromRole(string role)
        {
            var clients = await _userManageAppService.GetUsersInRole(role);
            return ResponseGet(clients);
        }

        [HttpDelete, Route("{role}/{username}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveUser(string role, string username)
        {
            var model = new RemoveUserFromRoleViewModel(role, username);
            await _roleManagerAppService.RemoveUserFromRole(model);
            return ResponseDelete();
        }
    }
}
