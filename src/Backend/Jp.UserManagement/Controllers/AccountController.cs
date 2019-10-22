using AutoMapper;
using Jp.Application.EventSourcedNormalizers;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Core.ViewModels;
using Jp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("accounts"), Authorize(Policy = "UserManagement")]
    public class AccountController : ApiController
    {
        private readonly IUserManageAppService _userAppService;
        private readonly IMapper _mapper;
        private readonly ISystemUser _systemUser;

        public AccountController(
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

        [HttpGet("profile")]
        public async Task<ActionResult<UserViewModel>> UserData()
        {
            var user = await _userAppService.GetUserAsync(_systemUser.UserId);
            return ResponseGet(user);
        }

        [HttpPut("profile")]
        public async Task<ActionResult> UpdateProfile([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Id = _systemUser.UserId;
            await _userAppService.UpdateProfile(model);
            return ResponsePutPatch();
        }

        [HttpPatch, Route("profile"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> PartialUpdate([FromBody] JsonPatchDocument<UserViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var actualUser = await _userAppService.GetUserDetails(_systemUser.Username);
            model.ApplyTo(actualUser);
            await _userAppService.UpdateProfile(actualUser);
            return ResponsePutPatch();
        }


        [HttpPut("profile/picture")]
        public async Task<ActionResult<bool>> UpdateProfilePicture([FromBody] ProfilePictureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Id = _systemUser.UserId;
            await _userAppService.UpdateProfilePicture(model);
            return ResponsePutPatch();
        }


        [HttpDelete("")]
        public async Task<ActionResult<bool>> RemoveAccount()
        {
            var model = new RemoveAccountViewModel(_systemUser.UserId);
            await _userAppService.RemoveAccount(model);
            return ResponseDelete();
        }

        [HttpPut("password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Id = _systemUser.UserId;
            await _userAppService.ChangePassword(model);
            return ResponsePutPatch();
        }

        [HttpPost("password")]
        public async Task<ActionResult<bool>> CreatePassword([FromBody] SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Id = _systemUser.UserId;
            await _userAppService.CreatePassword(model);
            return Ok();
        }

        [HttpGet("password")]
        public async Task<ActionResult<bool>> HasPassword()
        {
            return ResponseGet(await _userAppService.HasPassword(_systemUser.UserId));
        }

        [HttpGet, Route("logs")]
        public async Task<ActionResult<ListOf<EventHistoryData>>> GetLogs([Range(1, 50)] int? limit = 10, [Range(1, int.MaxValue)] int? offset = 1, string search = null)
        {
            return ResponseGet(await _userAppService.GetEvents(_systemUser.Username, new PagingViewModel(limit ?? 10, offset ?? 0, search)));
        }

    }

}