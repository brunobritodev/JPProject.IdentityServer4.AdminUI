using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Clients;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Commands.PersistedGrant;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class DomainCommandsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<RemovePersistedGrantCommand, bool>, PersistedGrantCommandHandler>();

            /*
             * Api Resource  commands
             */
            services.AddScoped<IRequestHandler<RegisterApiResourceCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateApiResourceCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiResourceCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiSecretCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<SaveApiSecretCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiScopeCommand, bool>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<SaveApiScopeCommand, bool>, ApiResourceCommandHandler>();

            /*
             * Identity Resource  commands
             */
            services.AddScoped<IRequestHandler<RegisterIdentityResourceCommand, bool>, IdentityResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveIdentityResourceCommand, bool>, IdentityResourceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateIdentityResourceCommand, bool>, IdentityResourceCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterApiResourceCommand, bool>, ApiResourceCommandHandler>();

            /*
             * Client commands
             */
            services.AddScoped<IRequestHandler<RemoveClientCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClientCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClientSecretCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientSecretCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePropertyCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientPropertyCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClientClaimCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientClaimCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientCommand, bool>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<CopyClientCommand, bool>, ClientCommandHandler>();
        }
    }
}
