using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Tools;
using Equinox.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    [Route("[controller]")]
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

        [HttpPost, Route("register")]
        public async Task<ActionResult<DefaultResponse<bool>>> Register([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userAppService.Register(model);

            return Response(true);
        }

        [HttpPost, Route("register-provider")]
        public async Task<ActionResult<DefaultResponse<bool>>> RegisterWithProvider([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userAppService.RegisterWithProvider(model);

            return Response(true);
        }

        [HttpGet, Route("checkUsername")]
        public async Task<ActionResult<DefaultResponse<bool>>> CheckUsername(string username)
        {
            var exist = await _userAppService.CheckUsername(username);

            return Response(exist);
        }

        [HttpGet, Route("checkEmail")]
        public async Task<ActionResult<DefaultResponse<bool>>> CheckEmail(string email)
        {
            var exist = await _userAppService.CheckUsername(email);

            return Response(exist);
        }

        [HttpPost, Route("forgot-password")]
        public async Task<ActionResult<DefaultResponse<bool>>> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userAppService.SendResetLink(model);

            return Response(true);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult<DefaultResponse<bool>>> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userAppService.ResetPassword(model);

            return Response(true);
        }

        [HttpPost, Route("confirm-email")]
        public async Task<ActionResult<DefaultResponse<bool>>> ConfirmEmail([FromBody] ConfirmEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userAppService.ConfirmEmail(model);
            return Response(true);
        }

        
    }
}