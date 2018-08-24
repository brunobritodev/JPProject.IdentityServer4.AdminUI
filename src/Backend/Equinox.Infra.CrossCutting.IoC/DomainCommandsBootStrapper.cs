using Equinox.Domain.CommandHandlers;
using Equinox.Domain.Commands;
using Equinox.Domain.Commands.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    internal class DomainCommandsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand>, CustomerCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewUserCommand>, UserCommandHandler>();
        }
    }

   
}
