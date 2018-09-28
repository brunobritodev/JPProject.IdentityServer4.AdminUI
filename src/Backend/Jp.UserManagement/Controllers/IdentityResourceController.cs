using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]"),
        // Authorize(Policy = "IS4-ReadOnly")
    ]
    public class IdentityResourceController: ApiController
    {
        private readonly IIdentityResourceAppService _identityResourceAppService;

        public IdentityResourceController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IIdentityResourceAppService identityResourceAppService) : base(notifications, mediator)
        {
            _identityResourceAppService = identityResourceAppService;
        }

        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<IdentityResource>>>> List()
        {
            var irs = await _identityResourceAppService.GetIdentityResources();
            return Response(irs);
        }
    }
}
