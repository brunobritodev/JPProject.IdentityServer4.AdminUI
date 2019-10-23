using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("identity-resources"), Authorize(Policy = "ReadOnly")]
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

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<IdentityResourceListView>>> List()
        {
            var irs = await _identityResourceAppService.GetIdentityResources();
            return ResponseGet(irs);
        }

        [HttpGet("{resource}")]
        public async Task<ActionResult<IdentityResource>> Details(string resource)
        {
            var irs = await _identityResourceAppService.GetDetails(resource);
            return ResponseGet(irs);
        }

        [HttpPost(""), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IdentityResource>> Save([FromBody] IdentityResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _identityResourceAppService.Save(model);
            var idr = await _identityResourceAppService.GetDetails(model.Name);
            return ResponsePost(nameof(Details), new { resource = model.Name }, idr);
        }

        [HttpPut("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Update(string resource, [FromBody] IdentityResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            await _identityResourceAppService.Update(resource, model);
            return ResponsePutPatch();
        }

        [HttpPatch("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> PartialUpdate(string resource, [FromBody] JsonPatchDocument<IdentityResource> model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var ir = await _identityResourceAppService.GetDetails(resource);
            if (ir == null)
            {
                ModelState.AddModelError("resource", "Invalid Api Resource");
                return ModelStateErrorResponseError();
            }

            model.ApplyTo(ir);
            await _identityResourceAppService.Update(resource, ir);
            return ResponsePutPatch();
        }

        [HttpDelete("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Remove(string resource)
        {
            var model = new RemoveIdentityResourceViewModel(resource);
            await _identityResourceAppService.Remove(model);
            return ResponseDelete();
        }
    }
}
