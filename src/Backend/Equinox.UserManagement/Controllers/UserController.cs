using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models.AccountViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserManagerAppService _userManagerAppService;

        public UserController(
            IUserManagerAppService userManagerAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManagerAppService = userManagerAppService;
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

           await _userManagerAppService.Register(model);

            return Response(model);
        }

        [HttpPost, Route("register-provider")]
        public async Task<IActionResult> RegisterWithProvider([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            await _userManagerAppService.RegisterWithProvider(model);

            return Response(model);
        }

        [HttpGet, Route("checkUsername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var exist = await _userManagerAppService.CheckUsername(username);

            return Response(exist);
        }

        [HttpGet, Route("checkEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var exist = await _userManagerAppService.CheckUsername(email);

            return Response(exist);
        }

        [HttpPost, Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }

            await _userManagerAppService.SendResetLink(model);

            return Response(model);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            await _userManagerAppService.ResetPassword(model);
            
            return Response(true);
        }

        [HttpPost, Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            await _userManagerAppService.ConfirmEmail(model);
            return Response(model);
        }
    }
}