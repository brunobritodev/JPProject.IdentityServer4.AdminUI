using Jp.Domain.Core.Notifications;
using Jp.Infra.CrossCutting.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class DomainEventsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<ICustomEventHandler, CustomEventHandler>();
        }
    }
}
