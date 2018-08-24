using Equinox.Domain.Core.Notifications;
using Equinox.Domain.EventHandlers;
using Equinox.Domain.Events.Customer;
using Equinox.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    internal class DomainEventsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            services.AddScoped<INotificationHandler<UserRegisteredeEvent>, UserEventHandler>();
        }
    }
}
