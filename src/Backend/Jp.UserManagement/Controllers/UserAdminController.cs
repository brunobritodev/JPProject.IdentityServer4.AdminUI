using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
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

        public UserAdminController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IUserManageAppService userManageAppService) : base(notifications, mediator)
        {
            _userManageAppService = userManageAppService;
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
    }
}