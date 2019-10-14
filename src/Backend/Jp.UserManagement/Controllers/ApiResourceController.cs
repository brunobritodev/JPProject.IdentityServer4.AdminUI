using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("api-resources"), Authorize(Policy = "ReadOnly")]
    [ApiConventionType(typeof(DefaultApiConventions))]
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


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResource>> Details(string id)
        {
            var irs = await _apiResourceAppService.GetDetails(id);
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

            return ResponsePost(nameof(Details), new { id = model.Name }, apires);
        }

        [HttpPut("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Update(string id, [FromBody] UpdateApiResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.OldApiResourceId = id;
            await _apiResourceAppService.Update(model);
            return ResponsePut();
        }

        [HttpDelete("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> Remove(string id)
        {
            var model = new RemoveApiResourceViewModel(id);
            await _apiResourceAppService.Remove(model);
            return ResponseDelete();
        }



        [HttpGet("{id}/secrets")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> Secrets(string id)
        {
            var clients = await _apiResourceAppService.GetSecrets(id);
            return ResponseGet(clients);
        }

        [HttpDelete("{id}/secrets/{secretId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<bool>> RemoveSecret(string id, int secretId)
        {
            var model = new RemoveApiSecretViewModel(id, secretId);
            await _apiResourceAppService.RemoveSecret(model);
            return ResponseDelete();
        }


        [HttpPost("{id}/secrets"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> SaveSecret(string id, [FromBody] SaveApiSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ResourceName = id;
            await _apiResourceAppService.SaveSecret(model);
            var secrets = await _apiResourceAppService.GetSecrets(id);
            return ResponsePost(nameof(Secrets), new { id }, secrets);
        }

        [HttpGet("{id}/scopes")]
        public async Task<ActionResult<IEnumerable<ScopeViewModel>>> Scopes(string id)
        {
            var clients = await _apiResourceAppService.GetScopes(id);
            return ResponseGet(clients);
        }

        [HttpDelete("{id}/scopes/{scopeId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveScope(string id, int scopeId)
        {
            var model = new RemoveApiScopeViewModel(id, scopeId);
            await _apiResourceAppService.RemoveScope(model);
            return ResponseDelete();
        }


        [HttpPost("{id}/scopes"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ScopeViewModel>>> SaveScope(string id, [FromBody] SaveApiScopeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ResourceName = id;
            await _apiResourceAppService.SaveScope(model);
            var scopes = await _apiResourceAppService.GetScopes(id);
            return ResponsePost(nameof(Scopes), new { id }, scopes);
        }


    }
}