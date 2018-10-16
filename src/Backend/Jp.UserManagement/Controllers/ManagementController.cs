using AutoMapper;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "UserManagement")]
    public class ManagementController : ApiController
    {
        private readonly IUserManageAppService _userAppService;
        private readonly IMapper _mapper;
        private readonly ISystemUser _systemUser;

        public ManagementController(
            IUserManageAppService userAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IMapper mapper,
            ISystemUser systemUser) : base(notifications, mediator)
        {
            _userAppService = userAppService;
            this._mapper = mapper;
            _systemUser = systemUser;
        }

        [Route("user-data"), HttpGet]
        public async Task<ActionResult<DefaultResponse<UserViewModel>>> UserData()
        {
            var user = await _userAppService.GetUserAsync(_systemUser.UserId);
            return Response(user);
        }

        [Route("update-profile"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateProfile([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.Id = _systemUser.UserId;
            await _userAppService.UpdateProfile(model);
            return Response(true);
        }


        [Route("update-profile-picture"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateProfilePicture([FromBody] ProfilePictureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.Id = _systemUser.UserId;
            await _userAppService.UpdateProfilePicture(model);
            return Response(true);
        }

        [Route("change-password"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.Id = _systemUser.UserId;
            await _userAppService.ChangePassword(model);
            return Response(true);
        }

        [Route("create-password"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> CreatePassword([FromBody] SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.Id = _systemUser.UserId;
            await _userAppService.CreatePassword(model);
            return Response(true);
        }
           
        [Route("remove-account"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveAccount()
        {
            var model = new RemoveAccountViewModel { Id = _systemUser.UserId };
            await _userAppService.RemoveAccount(model);
            return Response(true);
        }

        [HttpGet, Route("has-password")]
        public async Task<ActionResult<DefaultResponse<bool>>> HasPassword()
        {
            return Response(await _userAppService.HasPassword(_systemUser.UserId));
        }

        [HttpGet, Route("logs")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<EventHistoryData>>>> GetLogs()
        {
            return Response(await _userAppService.GetHistoryLogs(_systemUser.Username));
        }

    }

}