using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Core.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("persisted-grants"), Authorize(Policy = "ReadOnly")]
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

        [HttpGet, Route("")]
        public async Task<ActionResult<ListOfPersistedGrantViewModel>> List([Range(1, 50)] int? limit = 10, [Range(1, int.MaxValue)] int? offset = 1)
        {
            var irs = await _persistedGrantAppService.GetPersistedGrants(new PagingViewModel(limit ?? 10, offset ?? 0));
            return ResponseGet(irs);
        }

        [HttpDelete, Route("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Remove(string id)
        {
            var model = new RemovePersistedGrantViewModel(id);
            await _persistedGrantAppService.Remove(model);
            return ResponseDelete();
        }


    }
}
