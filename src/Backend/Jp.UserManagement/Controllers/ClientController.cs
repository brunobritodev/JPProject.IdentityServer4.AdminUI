using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
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
    [Route("clients"), Authorize(Policy = "ReadOnly")]
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

        [HttpGet("{client}")]
        public async Task<ActionResult<Client>> GetClient(string client)
        {
            var clients = await _clientAppService.GetClientDefaultDetails(client);
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

            return ResponsePost(nameof(GetClient), new { client = client.ClientId }, newClient);
        }

        [HttpPut("{client}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<ClientViewModel>> Update(string client, [FromBody] Client model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }


            await _clientAppService.Update(client, model);
            return ResponsePutPatch();
        }


        [HttpPatch("{client}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<ClientViewModel>> PartialUpdate(string client, [FromBody] JsonPatchDocument<Client> model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var clientDb = await _clientAppService.GetClientDetails(client);
            if (clientDb == null)
            {
                ModelState.AddModelError("client", "Invalid Api Resource");
                return ModelStateErrorResponseError();
            }

            model.ApplyTo(clientDb);
            await _clientAppService.Update(client, clientDb);
            return ResponsePutPatch();
        }

        [HttpDelete("{client}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> Delete(string client)
        {
            var command = new RemoveClientViewModel(client);
            await _clientAppService.Remove(command);
            return ResponseDelete();
        }


        [HttpPost("{client}/copy"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<Client>> Copy(string client)
        {
            var clientDb = new CopyClientViewModel(client);
            await _clientAppService.Copy(clientDb);
            var newClient = await _clientAppService.GetClientDetails(client);
            return ResponsePost(nameof(GetClient), new { client }, newClient);
        }

        [HttpGet("{client}/secrets")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> Secrets(string client)
        {
            var clients = await _clientAppService.GetSecrets(client);
            return ResponseGet(clients);
        }

        [HttpDelete("{client}/secrets/{secretId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveSecret(string client, int secretId)
        {
            var model = new RemoveClientSecretViewModel(client, secretId);
            await _clientAppService.RemoveSecret(model);
            return ResponseDelete();
        }

        [HttpPost("{client}/secrets"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<SecretViewModel>>> SaveSecret(string client, [FromBody] SaveClientSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveSecret(model);
            var secret = await _clientAppService.GetSecrets(client);
            return ResponsePost(nameof(Secrets), new { client }, secret);
        }

        [HttpGet("{client}/properties")]
        public async Task<ActionResult<IEnumerable<ClientPropertyViewModel>>> Properties(string client)
        {
            var clients = await _clientAppService.GetProperties(client);
            return ResponseGet(clients);
        }

        [HttpDelete("{client}/properties/{propertyId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveProperty(string client, int propertyId)
        {
            var model = new RemovePropertyViewModel(propertyId, client);
            await _clientAppService.RemoveProperty(model);
            return ResponseDelete();
        }


        [HttpPost("{client}/properties"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ClientPropertyViewModel>>> SaveProperty(string client, [FromBody] SaveClientPropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveProperty(model);
            var properties = await _clientAppService.GetProperties(client);
            return ResponsePost(nameof(Properties), new { client }, properties);
        }

        [HttpGet("{client}/claims")]
        public async Task<ActionResult<IEnumerable<ClaimViewModel>>> Claims(string client)
        {
            var clients = await _clientAppService.GetClaims(client);
            return ResponseGet(clients);
        }

        [HttpDelete("{client}/claims/{claimId:int}"), Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveClaim(string client, int claimId)
        {
            var model = new RemoveClientClaimViewModel(client, claimId);
            await _clientAppService.RemoveClaim(model);
            return ResponseDelete();
        }


        [HttpPost("{client}/claims"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<ClaimViewModel>>> SaveClaim(string client, [FromBody] SaveClientClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }
            await _clientAppService.SaveClaim(model);
            var claims = await _clientAppService.GetClaims(client);
            return ResponsePost(nameof(Claims), new { client }, claims);
        }

    }

}
