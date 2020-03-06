using IdentityServer4.Models;
using JPProject.Admin.Application.Interfaces;
using JPProject.Admin.Application.ViewModels.ClientsViewModels;
using JPProject.Domain.Core.Bus;
using JPProject.Domain.Core.Interfaces;
using JPProject.Domain.Core.Notifications;
using JPProject.Domain.Core.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPProject.Admin.Api.Controllers
{
    [Route("clients"), Authorize(Policy = "Default")]
    public class ClientsController : ApiController
    {
        private readonly IClientAppService _clientAppService;
        private readonly ISystemUser _user;
        private readonly IHttpContextAccessor _httpContext;

        public ClientsController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IClientAppService clientAppService,
            ISystemUser user,
            IHttpContextAccessor httpContext) : base(notifications, mediator)
        {
            _clientAppService = clientAppService;
            _user = user;
            _httpContext = httpContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ClientListViewModel>>> ListClients()
        {
            var at = await _httpContext.HttpContext.GetTokenAsync("access_token");
            var clients = await _clientAppService.GetClients();
            return ResponseGet(clients);
        }

        [HttpGet("{client}")]
        public async Task<ActionResult<Client>> GetClient(string client)
        {
            var clients = await _clientAppService.GetClientDefaultDetails(client);
            return ResponseGet(clients);
        }

        [HttpPost("")]
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

        [HttpPut("{client}")]
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

        [HttpPatch("{client}")]
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

        [HttpDelete("{client}")]
        public async Task<ActionResult> Delete(string client)
        {
            var command = new RemoveClientViewModel(client);
            await _clientAppService.Remove(command);
            return ResponseDelete();
        }


        [HttpPost("{client}/copy")]
        public async Task<ActionResult<Client>> Copy(string client)
        {
            var clientDb = new CopyClientViewModel(client);
            await _clientAppService.Copy(clientDb);
            var newClient = await _clientAppService.GetClientDetails(client);
            return ResponsePost(nameof(GetClient), new { client }, newClient);
        }

        [HttpGet("{client}/secrets")]
        public async Task<ActionResult<IEnumerable<Secret>>> Secrets(string client)
        {
            var clients = await _clientAppService.GetSecrets(client);
            return ResponseGet(clients);
        }

        [HttpDelete("{client}/secrets")]
        public async Task<ActionResult> RemoveSecret(string client, string type, string value)
        {
            var model = new RemoveClientSecretViewModel(client, type, value);
            await _clientAppService.RemoveSecret(model);
            return ResponseDelete();
        }

        [HttpPost("{client}/secrets")]
        public async Task<ActionResult<IEnumerable<Secret>>> SaveSecret(string client, [FromBody] SaveClientSecretViewModel model)
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

        [HttpDelete("{client}/properties/{key}")]
        public async Task<ActionResult> RemoveProperty(string client, string key)
        {
            var model = new RemovePropertyViewModel(key, client);
            await _clientAppService.RemoveProperty(model);
            return ResponseDelete();
        }


        [HttpPost("{client}/properties")]
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

        [HttpDelete("{client}/claims")]
        public async Task<ActionResult> RemoveClaim(string client, string type, string value)
        {
            var model = new RemoveClientClaimViewModel(client, type, value);
            await _clientAppService.RemoveClaim(model);
            return ResponseDelete();
        }


        [HttpPost("{client}/claims")]
        public async Task<ActionResult<IEnumerable<ClaimViewModel>>> SaveClaim(string client, [FromBody] SaveClientClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ClientId = client;
            await _clientAppService.SaveClaim(model);
            var claims = await _clientAppService.GetClaims(client);
            return ResponsePost(nameof(Claims), new { client }, claims);
        }

    }
}
