using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.Interfaces;
using Jp.Application.ViewModels;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Tools.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jp.Management.Controllers
{
    [Authorize(Policy = "IS4-ReadOnly")]
    public class ClientController : ApiController
    {
        private readonly IClientAppService _clientAppService;

        public ClientController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
                IClientAppService clientAppService) : base(notifications, mediator)
        {
            _clientAppService = clientAppService;
        }

        public async Task<ActionResult<DefaultResponse<Client>>> GetClients()
        {
            return Ok(await _clientAppService.GetClients());
        }
    }
}
