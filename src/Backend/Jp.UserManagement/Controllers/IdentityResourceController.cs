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
    [ApiConventionType(typeof(DefaultApiConventions))]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityResource>> Details(string id)
        {
            var irs = await _identityResourceAppService.GetDetails(id);
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
            return ResponsePost(nameof(Details), new { id = model.Name }, idr);
        }

        [HttpPut("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Update(string id, [FromBody] IdentityResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            await _identityResourceAppService.Update(id, model);
            return ResponsePutPatch();
        }

        [HttpPatch("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> PartialUpdate(string id, [FromBody] JsonPatchDocument<IdentityResource> model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var ir = await _identityResourceAppService.GetDetails(id);
            if (ir == null)
            {
                ModelState.AddModelError("id", "Invalid Api Resource");
                return ModelStateErrorResponseError();
            }

            model.ApplyTo(ir);
            await _identityResourceAppService.Update(id, ir);
            return ResponsePutPatch();
        }

        [HttpDelete("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Remove(string id)
        {
            var model = new RemoveIdentityResourceViewModel(id);
            await _identityResourceAppService.Remove(model);
            return ResponseDelete();
        }
    }
}
