using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]")]
    public class UserAdminController : ApiController
    {
        private readonly IUserManageAppService _userManageAppService;
        private readonly ISystemUser _user;
        private readonly IRoleManagerAppService _roleManagerAppService;

        public UserAdminController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IUserManageAppService userManageAppService,
            ISystemUser user,
            IRoleManagerAppService roleManagerAppService) : base(notifications, mediator)
        {
            _userManageAppService = userManageAppService;
            _user = user;
            _roleManagerAppService = roleManagerAppService;
        }


        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<UserListViewModel>>>> List()
        {
            var irs = await _userManageAppService.GetUsers();
            return Response(irs);
        }

        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<UserViewModel>>> Details(string username)
        {
            var irs = await _userManageAppService.GetUserDetails(username);
            return Response(irs);
        }


        [HttpPost, Route("update")]
        public async Task<ActionResult<DefaultResponse<bool>>> Update([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _userManageAppService.UpdateUser(model);
            return Response(true);
        }


        [Route("remove-account"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveAccount([FromBody] RemoveAccountViewModel model)
        {
            await _userManageAppService.RemoveAccount(model);
            return Response(true);
        }


        [HttpGet, Route("claims")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClaimViewModel>>>> Claims(string userName)
        {
            var clients = await _userManageAppService.GetClaims(userName);
            return Response(clients);
        }

        [HttpPost, Route("remove-claim")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveClaim([FromBody] RemoveUserClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _userManageAppService.RemoveClaim(model);
            return Response(true);
        }


        [HttpPost, Route("save-claim")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveClaim([FromBody] SaveUserClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _userManageAppService.SaveClaim(model);
            return Response(true);
        }

        [HttpGet, Route("roles")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<RoleViewModel>>>> Roles(string userName)
        {
            var clients = await _userManageAppService.GetRoles(userName);
            return Response(clients);
        }

        [HttpPost, Route("remove-role")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveRole([FromBody] RemoveUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.UserId = _user.UserId;
            await _userManageAppService.RemoveRole(model);
            return Response(true);
        }


        [HttpPost, Route("save-role")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveRole([FromBody] SaveUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _userManageAppService.SaveRole(model);
            return Response(true);
        }

        [HttpGet, Route("all-roles")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<RoleViewModel>>>> AllRoles()
        {
            var clients = await _roleManagerAppService.GetAllRoles();
            return Response(clients);
        }
    }
}