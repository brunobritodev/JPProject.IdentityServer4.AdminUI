using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Route("[controller]"),
    // Authorize(Policy = "IS4-ReadOnly")
    ]
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

        [HttpGet, Route("details")]
        public async Task<ActionResult<DefaultResponse<Client>>> Details(string clientId)
        {
            var clients = await _clientAppService.GetClientDetails(clientId);
            return Response(clients);
        }

        [HttpGet, Route("secrets")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<SecretViewModel>>>> Secrets(string clientId)
        {
            var clients = await _clientAppService.GetSecrets(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-secret")]
        public async Task<ActionResult<DefaultResponse<bool>>> RemoveSecret([FromBody] RemoveSecretViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.RemoveSecret(model);
            return Response(true);
        }


        [HttpPost, Route("save-secret")]
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



        [HttpPost, Route("update")]
        public async Task<ActionResult<DefaultResponse<bool>>> Update([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(false);
            }
            await _clientAppService.Update(client);
            return Response(true);
        }

        [HttpGet, Route("properties")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClientPropertyViewModel>>>> Properties(string clientId)
        {
            var clients = await _clientAppService.GetProperties(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-property")]
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


        [HttpPost, Route("save-property")]
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
        public async Task<ActionResult<DefaultResponse<IEnumerable<ClientClaimViewModel>>>> Claims(string clientId)
        {
            var clients = await _clientAppService.GetClaims(clientId);
            return Response(clients);
        }

        [HttpPost, Route("remove-claim")]
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


        [HttpPost, Route("save-claim")]
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
