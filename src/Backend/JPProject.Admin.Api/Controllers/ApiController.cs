using System.Collections.Generic;
using System.Linq;
using JPProject.Domain.Core.Bus;
using JPProject.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JPProject.Admin.Api.Controllers
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


        protected ActionResult ResponsePutPatch()
        {
            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { nameof(DomainNotification), _notifications.GetNotifications().Select(n => n.Value).ToArray() }
            }));
        }

        protected ActionResult ResponseDelete()
        {
            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { nameof(DomainNotification), _notifications.GetNotifications().Select(n => n.Value).ToArray() }
            }));
        }

        protected ActionResult<T> ResponsePost<T>(string action, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, route, result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { nameof(DomainNotification), _notifications.GetNotifications().Select(n => n.Value).ToArray() }
            }));
        }

        protected ActionResult<T> ResponsePost<T>(string action, string controller, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, controller, route, result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { nameof(DomainNotification), _notifications.GetNotifications().Select(n => n.Value).ToArray() }
            }));
        }
        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
        {

            if (result == null || (result != null && !result.Any()))
                return NoContent();

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (result == null)
                return NotFound();

            return Ok(result);
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

        protected ActionResult ModelStateErrorResponseError()
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }


        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }


    }
}