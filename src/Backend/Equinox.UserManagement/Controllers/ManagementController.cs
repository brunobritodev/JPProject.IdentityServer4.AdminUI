using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.CrossCutting.Tools.Model;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    [Route("[controller]"), Authorize]
    public class ManagementController : ApiController
    {
        private readonly IUserManageAppService _userAppService;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public ManagementController(
            IUserManageAppService userAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IUserManager userManager,
            IMapper mapper) : base(notifications, mediator)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            this._mapper = mapper;
        }

        [Route("user-data"), HttpGet]
        public async Task<ActionResult<DefaultResponse<ProfileViewModel>>> UserData()
        {
            var user = await _userManager.GetUserAsync(GetUserId().Value);
            return Response(_mapper.Map<ProfileViewModel>(user));
        }

        [Route("update-profile"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateProfile([FromBody] ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            model.Id = GetUserId();
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

            model.Id = GetUserId();
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

            model.Id = GetUserId();
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

            model.Id = GetUserId();
            await _userAppService.CreatePassword(model);
            return Response(true);
        }

        [Route("remove-account"), HttpPost]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveAccount()
        {
            var model = new RemoveAccountViewModel { Id = GetUserId() };
            await _userAppService.RemoveAccount(model);
            return Response(true);
        }

        [HttpGet, Route("has-password")]
        public async Task<ActionResult<DefaultResponse<bool>>> HasPassword() => Response(await _userAppService.HasPassword(GetUserId().Value));

        [HttpGet, Route("logs")]
        public ActionResult<DefaultResponse<IEnumerable<EventHistoryData>>> GetLogs()
        {
            return Response(_userAppService.GetHistoryLogs(GetUserId().Value));
        }

    }

}