using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
    public class PersistedGrantsController : ApiController
    {
        private readonly IPersistedGrantAppService _persistedGrantAppService;

        public PersistedGrantsController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IPersistedGrantAppService persistedGrantAppService) : base(notifications, mediator)
        {
            _persistedGrantAppService = persistedGrantAppService;
        }


        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<PersistedGrantViewModel>>>> List()
        {
            var irs = await _persistedGrantAppService.GetPersistedGrants();
            return Response(irs);
        }

        [HttpPost, Route("remove"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Remove([FromBody] RemovePersistedGrantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _persistedGrantAppService.Remove(model);
            return Response(true);
        }


    }
}
