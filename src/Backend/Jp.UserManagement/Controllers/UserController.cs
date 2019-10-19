using Jp.Application.Interfaces;
using Jp.Application.ViewModels.UserViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{

    [Route("user"), AllowAnonymous]
    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public UserController(
            IUserAppService userAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userAppService = userAppService;
        }

        [HttpPost, Route("{username}/password/forget")]
        public async Task<ActionResult> ForgotPassword(string username)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            var model = new ForgotPasswordViewModel(username);
            await _userAppService.SendResetLink(model);

            return Ok();
        }

        [HttpPost, Route("{username}/password/reset")]
        public async Task<ActionResult> ResetPassword(string username, [FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Email = username;
            await _userAppService.ResetPassword(model);

            return Ok();
        }

        [HttpPost, Route("{username}/confirm-email")]
        public async Task<ActionResult> ConfirmEmail(string username, [FromBody] ConfirmEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.Email = username;
            await _userAppService.ConfirmEmail(model);
            return Ok();
        }

    }
}