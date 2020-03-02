using JPProject.Admin.Application.Interfaces;
using JPProject.Admin.Application.ViewModels;
using JPProject.Domain.Core.Bus;
using JPProject.Domain.Core.Notifications;
using JPProject.Domain.Core.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace JPProject.Admin.Api.Controllers
{
    [Route("persisted-grants"), Authorize(Policy = "Default")]
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
        public async Task<ActionResult<ListOf<PersistedGrantViewModel>>> List([Range(1, 50)] int? limit = 10, [Range(1, int.MaxValue)] int? offset = 1)
        {
            var searchPersisted = new PersistedGrantSearch()
            {
                Limit = limit,
                Offset = offset
            };
            var irs = await _persistedGrantAppService.GetPersistedGrants(searchPersisted);

            return ResponseGet(irs);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            var model = new RemovePersistedGrantViewModel(id);
            await _persistedGrantAppService.Remove(model);
            return ResponseDelete();
        }


    }
}
