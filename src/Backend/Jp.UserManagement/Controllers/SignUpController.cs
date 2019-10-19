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
    [Route("sign-up"), AllowAnonymous]
    public class SignUpController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public SignUpController(
            IUserAppService userAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userAppService = userAppService;
        }

        [HttpPost, Route("")]
        public async Task<ActionResult<RegisterUserViewModel>> Register([FromBody] RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            if (model.ContainsFederationGateway())
                await _userAppService.RegisterWithProvider(model);
            else
                await _userAppService.Register(model);

            model.ClearSensitiveData();
            return ResponsePost("profile", "account", null, model);
        }


        [HttpGet, Route("check-username/{suggestedUsername}")]
        public async Task<ActionResult<bool>> CheckUsername(string suggestedUsername)
        {
            var exist = await _userAppService.CheckUsername(suggestedUsername);

            return ResponseGet(exist);
        }

        [HttpGet, Route("check-email/{givenEmail}")]
        public async Task<ActionResult<bool>> CheckEmail(string givenEmail)
        {
            var exist = await _userAppService.CheckUsername(givenEmail);

            return ResponseGet(exist);
        }
    }
}