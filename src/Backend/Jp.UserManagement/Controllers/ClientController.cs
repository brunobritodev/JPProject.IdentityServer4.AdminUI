using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
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
    [Route("clients"), Authorize(Policy = "ReadOnly")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ClientsController : ApiController
    {
        private readonly IClientAppService _clientAppService;

        public ClientsController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IClientAppService clientAppService) : base(notifications, mediator)
        {
            _clientAppService = clientAppService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ClientListViewModel>>> ListClients()
        {
            var clients = await _clientAppService.GetClients();
            return ResponseGet(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(string id)
        {
            var clients = await _clientAppService.GetClientDefaultDetails(id);
            return ResponseGet(clients);
        }

        [HttpPost(""), Authorize(Policy = "Admin")]
        public async Task<ActionResult<Client>> Post([FromBody] SaveClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.Save(client);
            var newClient = await _clientAppService.GetClientDetails(client.ClientId);

            return ResponsePost(nameof(GetClient), new { id = client.ClientId }, newClient);
        }

        [HttpPut("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<ClientViewModel>> Put(string id, [FromBody] ClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            client.OldClientId = id;
            await _clientAppService.Update(client);
            return ResponsePut();
        }

        [HttpDelete("{id}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var command = new RemoveClientViewModel(id);
            await _clientAppService.Remove(command);
            return ResponseDelete();
        }


        [HttpPost("{id}/copy"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<Client>> Copy(string id)
        {
            var client = new CopyClientViewModel(id);
            await _clientAppService.Copy(client);
            var newClient = await _clientAppService.GetClientDetails(id);
            return ResponsePost(nameof(GetClient), new { id }, newClient);
        }

        [HttpGet("{id}/secrets")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> Secrets(string id)
        {
            var clients = await _clientAppService.GetSecrets(id);
            return ResponseGet(clients);
        }

        [HttpDelete("{id}/secrets/{secretId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveSecret(string id, int secretId)
        {
            var model = new RemoveClientSecretViewModel(id, secretId);
            await _clientAppService.RemoveSecret(model);
            return ResponseDelete();
        }

        [HttpPost("{id}/secrets"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> SaveSecret(string id, [FromBody] SaveClientSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveSecret(model);
            var secret = await _clientAppService.GetSecrets(id);
            return ResponsePost(nameof(Secrets), new { id }, secret);
        }

        [HttpGet("{id}/properties")]
        public async Task<ActionResult<IEnumerable<ClientPropertyViewModel>>> Properties(string id)
        {
            var clients = await _clientAppService.GetProperties(id);
            return ResponseGet(clients);
        }

        [HttpDelete("{id}/properties/{propertyId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveProperty(string id, int propertyId)
        {
            var model = new RemovePropertyViewModel(propertyId, id);
            await _clientAppService.RemoveProperty(model);
            return ResponseDelete();
        }


        [HttpPost("{id}/properties"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ClientPropertyViewModel>>> SaveProperty(string id, [FromBody] SaveClientPropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveProperty(model);
            var properties = await _clientAppService.GetProperties(id);
            return ResponsePost(nameof(Properties), new { id }, properties);
        }

        [HttpGet("{id}/claims")]
        public async Task<ActionResult<IEnumerable<ClaimViewModel>>> Claims(string id)
        {
            var clients = await _clientAppService.GetClaims(id);
            return ResponseGet(clients);
        }

        [HttpDelete("{id}/claims/{claimId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveClaim(string id, int claimId)
        {
            var model = new RemoveClientClaimViewModel(id, claimId);
            await _clientAppService.RemoveClaim(model);
            return ResponseDelete();
        }


        [HttpPost("{id}/claims"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ClaimViewModel>>> SaveClaim(string id, [FromBody] SaveClientClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveClaim(model);
            var claims = await _clientAppService.GetClaims(id);
            return ResponsePost(nameof(Claims), new { id }, claims);
        }

    }

}
