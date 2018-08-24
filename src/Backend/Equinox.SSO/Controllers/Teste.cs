using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.SSO.Controllers
{
    [Route("[controller]")]
    public class Teste : ApiController
    {
        public Teste(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
        }

        [HttpGet, Route("ping"), Authorize]
        public IActionResult Ping()
        {
            return Response("pong");
        }
    }
}
