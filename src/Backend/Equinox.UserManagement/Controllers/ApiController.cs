using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Tools;
using Equinox.Infra.CrossCutting.Tools.Model;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UserManagement.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        protected ApiController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
        
        protected new ActionResult<DefaultResponse<T>> Response<T>(T result)
        {
            if (IsValidOperation())
            {
                return Ok(new DefaultResponse<T>
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new DefaultResponse<T>
            {
                Success = false,
                Errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }

        protected Guid? GetUserId()
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            return Guid.Parse(User.FindFirst(JwtClaimTypes.Subject)?.Value);
        }

    }
}