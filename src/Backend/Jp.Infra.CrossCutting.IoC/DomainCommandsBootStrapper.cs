using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Client;
using Jp.Domain.Commands.IdentityResource;
using Jp.Domain.Commands.PersistedGrant;
using Jp.Domain.Commands.Role;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class DomainCommandsBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<RemovePersistedGrantCommand>, PersistedGrantCommandHandler>();

            /*
             * Role commands
             */
            services.AddScoped<IRequestHandler<RemoveRoleCommand>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserFromRoleCommand>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRoleCommand>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<SaveRoleCommand>, RoleCommandHandler>();
            
            

            /*
             * Api Resource  commands
             */
            services.AddScoped<IRequestHandler<RegisterApiResourceCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateApiResourceCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiResourceCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiSecretCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<SaveApiSecretCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveApiScopeCommand>, ApiResourceCommandHandler>();
            services.AddScoped<IRequestHandler<SaveApiScopeCommand>, ApiResourceCommandHandler>();

            /*
             * Identity Resource  commands
             */
            services.AddScoped<IRequestHandler<RegisterIdentityResourceCommand>, IdentityResourceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveIdentityResourceCommand>, IdentityResourceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateIdentityResourceCommand>, IdentityResourceCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterApiResourceCommand>, ApiResourceCommandHandler>();

            /*
             * Client commands
             */
            services.AddScoped<IRequestHandler<RemoveClientCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClientCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClientSecretCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientSecretCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePropertyCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientPropertyCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClientClaimCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientClaimCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<SaveClientCommand>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<CopyClientCommand>, ClientCommandHandler>();

            /*
             * Regiser commands
             */
            services.AddScoped<IRequestHandler<RegisterNewUserCommand>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewUserWithoutPassCommand>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewUserWithProviderCommand>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<SendResetLinkCommand>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<ResetPasswordCommand>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<ConfirmEmailCommand>, UserCommandHandler>();
            

            /*
             * User manager
             */
            services.AddScoped<IRequestHandler<UpdateProfileCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProfilePictureCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SetPasswordCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<ChangePasswordCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAccountCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SaveUserClaimCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserClaimCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SaveUserRoleCommand>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserRoleCommand>, UserManagementCommandHandler>();
        }
    }
}
