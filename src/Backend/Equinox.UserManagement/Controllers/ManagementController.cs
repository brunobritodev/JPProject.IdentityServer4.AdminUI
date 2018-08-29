using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    [Route("[controller]"), Authorize]
    public class ManagementController : ApiController
    {
        private readonly IUserManager _userManager;

        public ManagementController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IUserManager userManager) : base(notifications, mediator)
        {
            _userManager = userManager;
        }

        [Route("user-info"), HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            return Response(user);
        }
    }
}