using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Application.Interfaces;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet, Route("search")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<string>>>> Search(string search)
        {
            var clients = await _scopesAppService.GetScopes(search);
            return Response(clients);
        }
    }
}