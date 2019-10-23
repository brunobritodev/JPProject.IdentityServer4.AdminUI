using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
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
    [Route("api-resources"), Authorize(Policy = "ReadOnly")]
    public class ApiResourcesController : ApiController
    {
        private readonly IApiResourceAppService _apiResourceAppService;

        public ApiResourcesController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IApiResourceAppService apiResourceAppService) : base(notifications, mediator)
        {
            _apiResourceAppService = apiResourceAppService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ApiResourceListViewModel>>> List()
        {
            var irs = await _apiResourceAppService.GetApiResources();
            return ResponseGet(irs);
        }


        [HttpGet("{resource}")]
        public async Task<ActionResult<ApiResource>> Details(string resource)
        {
            var irs = await _apiResourceAppService.GetDetails(resource);
            return ResponseGet(irs);
        }

        [HttpPost(""), Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResource>> Save([FromBody] ApiResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _apiResourceAppService.Save(model);
            var apires = await _apiResourceAppService.GetDetails(model.Name);

            return ResponsePost(nameof(Details), new { resource = model.Name }, apires);
        }

        [HttpPut("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Update(string resource, [FromBody] ApiResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            await _apiResourceAppService.Update(resource, model);
            return ResponsePutPatch();
        }

        [HttpPatch("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> PartialUpdate(string resource, [FromBody] JsonPatchDocument<ApiResource> model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var ar = await _apiResourceAppService.GetDetails(resource);
            if (ar == null)
            {
                ModelState.AddModelError("resource", "Invalid Api Resource");
                return ModelStateErrorResponseError();
            }
            model.ApplyTo(ar);
            await _apiResourceAppService.Update(resource, ar);
            return ResponsePutPatch();
        }

        [HttpDelete("{resource}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Remove(string resource)
        {
            var model = new RemoveApiResourceViewModel(resource);
            await _apiResourceAppService.Remove(model);
            return ResponseDelete();
        }



        [HttpGet("{resource}/secrets")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> Secrets(string resource)
        {
            var clients = await _apiResourceAppService.GetSecrets(resource);
            return ResponseGet(clients);
        }

        [HttpDelete("{resource}/secrets/{secretId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> RemoveSecret(string resource, int secretId)
        {
            var model = new RemoveApiSecretViewModel(resource, secretId);
            await _apiResourceAppService.RemoveSecret(model);
            return ResponseDelete();
        }


        [HttpPost("{resource}/secrets"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> SaveSecret(string resource, [FromBody] SaveApiSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ResourceName = resource;
            await _apiResourceAppService.SaveSecret(model);
            var secrets = await _apiResourceAppService.GetSecrets(resource);
            return ResponsePost(nameof(Secrets), new { resource }, secrets);
        }

        [HttpGet("{resource}/scopes")]
        public async Task<ActionResult<IEnumerable<ScopeViewModel>>> Scopes(string resource)
        {
            var clients = await _apiResourceAppService.GetScopes(resource);
            return ResponseGet(clients);
        }

        [HttpDelete("{resource}/scopes/{scopeId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveScope(string resource, int scopeId)
        {
            var model = new RemoveApiScopeViewModel(resource, scopeId);
            await _apiResourceAppService.RemoveScope(model);
            return ResponseDelete();
        }


        [HttpPost("{resource}/scopes"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ScopeViewModel>>> SaveScope(string resource, [FromBody] SaveApiScopeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ResourceName = resource;
            await _apiResourceAppService.SaveScope(model);
            var scopes = await _apiResourceAppService.GetScopes(resource);
            return ResponsePost(nameof(Scopes), new { resource }, scopes);
        }


    }
}