using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
    public class IdentityResourceController : ApiController
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
        public async Task<ActionResult<DefaultResponse<IEnumerable<IdentityResourceListView>>>> List()
        {
            var irs = await _identityResourceAppService.GetIdentityResources();
            return Response(irs);
        }

        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<IdentityResource>>> Details(string name)
        {
            var irs = await _identityResourceAppService.GetDetails(name);
            return Response(irs);
        }

        [HttpPost, Route("save"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Save([FromBody] IdentityResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _identityResourceAppService.Save(model);
            return Response(true);
        }

        [HttpPut, Route("update"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Update([FromBody] IdentityResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _identityResourceAppService.Update(model);
            return Response(true);
        }

        [HttpPost, Route("remove"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Remove([FromBody] RemoveIdentityResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _identityResourceAppService.Remove(model);
            return Response(true);
        }
    }
}
