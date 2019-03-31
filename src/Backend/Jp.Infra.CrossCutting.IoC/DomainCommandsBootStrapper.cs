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
            services.AddScoped<IRequestHandler<RemovePersistedGrantCommand, bool>, PersistedGrantCommandHandler>();

            /*
             * Role commands
             */
            services.AddScoped<IRequestHandler<RemoveRoleCommand, bool>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserFromRoleCommand, bool>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRoleCommand, bool>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<SaveRoleCommand, bool>, RoleCommandHandler>();
            
            

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

            /*
             * Regiser commands
             */
            services.AddScoped<IRequestHandler<RegisterNewUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewUserWithoutPassCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewUserWithProviderCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<SendResetLinkCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<ResetPasswordCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<ConfirmEmailCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<AddLoginCommand, bool>, UserCommandHandler>();
            

            /*
             * User manager
             */
            services.AddScoped<IRequestHandler<UpdateProfileCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProfilePictureCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SetPasswordCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<ChangePasswordCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAccountCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SaveUserClaimCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserClaimCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<SaveUserRoleCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserRoleCommand, bool>, UserManagementCommandHandler>();
            services.AddScoped<IRequestHandler<AdminChangePasswordCommand, bool>, UserManagementCommandHandler>();
        }
    }
}
