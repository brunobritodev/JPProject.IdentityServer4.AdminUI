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
    [Route("[controller]"), Authorize(Policy = "ReadOnly")]
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

        [HttpGet, Route("list")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClientListViewModel>>>> List()
        {
            var clients = await _clientAppService.GetClients();
            return Response(clients);
        }

        [HttpPost, Route("save"), Authorize(Policy = "Admin")
        ]
        public async Task<ActionResult<DefaultResponse<bool>>> Save([FromBody] SaveClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.Save(client);
            return Response(true);
        }

        [HttpPut, Route("update"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Update([FromBody] ClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.Update(client);
            return Response(true);
        }

        [HttpPost, Route("remove"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Remove([FromBody] RemoveClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.Remove(client);
            return Response(true);
        }

        [HttpPost, Route("copy"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> Copy([FromBody] CopyClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.Copy(client);
            return Response(true);
        }

        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<Client>>> Details(string clientId)
        {
            var clients = await _clientAppService.GetClientDefaultDetails(clientId);
            return Response(clients);
        }

        [HttpGet, Route("secrets")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<SecretViewModel>>>> Secrets(string clientId)
        {
            var clients = await _clientAppService.GetSecrets(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-secret"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveSecret([FromBody] RemoveClientSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.RemoveSecret(model);
            return Response(true);
        }


        [HttpPost, Route("save-secret"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveSecret([FromBody] SaveClientSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.SaveSecret(model);
            return Response(true);
        }

        [HttpGet, Route("properties")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClientPropertyViewModel>>>> Properties(string clientId)
        {
            var clients = await _clientAppService.GetProperties(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-property"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveProperty([FromBody] RemovePropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.RemoveProperty(model);
            return Response(true);
        }


        [HttpPost, Route("save-property"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveProperty([FromBody] SaveClientPropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.SaveProperty(model);
            return Response(true);
        }

        [HttpGet, Route("claims")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClaimViewModel>>>> Claims(string clientId)
        {
            var clients = await _clientAppService.GetClaims(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-claim"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveClaim([FromBody] RemoveClientClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.RemoveClaim(model);
            return Response(true);
        }


        [HttpPost, Route("save-claim"), Authorize(Policy = "Admin")]
        public async Task<ActionResult<DefaultResponse<bool>>> SaveClaim([FromBody] SaveClientClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.SaveClaim(model);
            return Response(true);
        }

    }

}
