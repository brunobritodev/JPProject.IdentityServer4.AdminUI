using System.Collections.Generic;
using System.Threading.Tasks;
using JPProject.Admin.Application.Interfaces;
using JPProject.Domain.Core.Bus;
using JPProject.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JPProject.Admin.Api.Controllers
{
    [Route("[controller]"), Authorize(Policy = "Default")]
    public class ScopesController : ApiController
    {
        private readonly IScopesAppService _scopesAppService;

        public ScopesController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IScopesAppService scopesAppService
            ) : base(notifications, mediator)
        {
            _scopesAppService = scopesAppService;
        }

        [HttpGet, Route("{scope}")]
        public async Task<ActionResult<IEnumerable<string>>> Search(string scope)
        {
            var clients = await _scopesAppService.GetScopes(scope);
            return ResponseGet(clients);
        }
    }
}