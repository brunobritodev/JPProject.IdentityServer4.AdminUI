using Jp.Domain.Core.Notifications;
using Jp.Domain.EventHandlers;
using Jp.Domain.Events.ApiResource;
using Jp.Domain.Events.Client;
using Jp.Domain.Events.IdentityResource;
using Jp.Domain.Events.PersistedGrant;
using Jp.Domain.Events.User;
using Jp.Domain.Events.UserManagement;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class DomainEventsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<PersistedGrantRemovedEvent>, PersistedGrantEventHandler>();
            services.AddScoped<INotificationHandler<IdentityResourceRegisteredEvent>, IdentityResourceEventHandler>();
            services.AddScoped<INotificationHandler<ClientRemovedEvent>, ClientEventHandler>();
            services.AddScoped<INotificationHandler<ClientUpdatedEvent>, ClientEventHandler>();

            services.AddScoped<INotificationHandler<ApiResourceRegisteredEvent>, ApiResourceEventHandler>();
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
