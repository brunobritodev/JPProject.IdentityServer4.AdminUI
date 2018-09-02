using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Commands;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        internal readonly IMediatorHandler Bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            Bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            Bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}