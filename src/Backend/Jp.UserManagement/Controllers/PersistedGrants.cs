using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
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
    public class PersistedGrants : ApiController
    {
        private readonly IPersistedGrantAppService _persistedGrantAppService;

        public PersistedGrants(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IPersistedGrantAppService persistedGrantAppService) : base(notifications, mediator)
        {
            _persistedGrantAppService = persistedGrantAppService;
        }


        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<PersistedGrant>>>> List()
        {
            var irs = await _persistedGrantAppService.GetPersistedGrants();
            return Response(irs);
        }

        //[HttpGet, Route("details")]
        //public async Task<ActionResult<DefaultResponse<IdentityResource>>> Details(string name)
        //{
        //    var irs = await _persistedGrantAppService.GetDetails(name);
        //    return Response(irs);
        //}

    }
}
