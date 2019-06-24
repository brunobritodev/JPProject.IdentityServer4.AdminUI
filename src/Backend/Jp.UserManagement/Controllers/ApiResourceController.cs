using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Management.Controllers
{
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
    public class ApiResourceController : ApiController
    {
        private readonly IApiResourceAppService _apiResourceAppService;

        public ApiResourceController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IApiResourceAppService apiResourceAppService) : base(notifications, mediator)
        {
            _apiResourceAppService = apiResourceAppService;
        }

        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ApiResourceListViewModel>>>> List()
        {
            var irs = await _apiResourceAppService.GetApiResources();
            return Response(irs);
        }


        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<ApiResource>>> Details(string name)
        {
            var irs = await _apiResourceAppService.GetDetails(name);
            return Response(irs);
        }

        [HttpPost, Route("save"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Save([FromBody] ApiResource model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.Save(model);
            return Response(true);
        }

        [HttpPut, Route("update"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Update([FromBody] UpdateApiResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.Update(model);
            return Response(true);
        }

        [HttpPost, Route("remove"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Remove([FromBody] RemoveApiResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.Remove(model);
            return Response(true);
        }



        [HttpGet, Route("secrets")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<SecretViewModel>>>> Secrets(string name)
        {
            var clients = await _apiResourceAppService.GetSecrets(name);
            return Response(clients);
        }

        [HttpPost, Route("remove-secret"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveSecret([FromBody] RemoveApiSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.RemoveSecret(model);
            return Response(true);
        }


        [HttpPost, Route("save-secret"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveSecret([FromBody] SaveApiSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.SaveSecret(model);
            return Response(true);
        }

        [HttpGet, Route("scopes")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ScopeViewModel>>>> Scopes(string name)
        {
            var clients = await _apiResourceAppService.GetScopes(name);
            return Response(clients);
        }

        [HttpPost, Route("remove-scope"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveScope([FromBody] RemoveApiScopeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.RemoveScope(model);
            return Response(true);
        }


        [HttpPost, Route("save-scope"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveScope([FromBody] SaveApiScopeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _apiResourceAppService.SaveScope(model);
            return Response(true);
        }


    }
}