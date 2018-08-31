using Equinox.Domain.Core.Notifications;
using Equinox.Domain.EventHandlers;
using Equinox.Domain.Events.User;
using Equinox.Domain.Events.UserManagement;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    internal class DomainEventsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<EmailConfirmedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<ProfileUpdatedEvent>, UserManagerEventHandler>();
            services.AddScoped<INotificationHandler<ProfilePictureUpdatedEvent>, UserManagerEventHandler>();
            services.AddScoped<INotificationHandler<PasswordCreatedEvent>, UserManagerEventHandler>();
            services.AddScoped<INotificationHandler<PasswordChangedEvent>, UserManagerEventHandler>();
            services.AddScoped<INotificationHandler<AccountRemovedEvent>, UserManagerEventHandler>();
        }
    }
}
