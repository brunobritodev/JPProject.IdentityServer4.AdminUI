using Jp.Application.Interfaces;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
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