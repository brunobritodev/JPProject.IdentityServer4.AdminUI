using Jp.Domain.CommandHandlers;
using Jp.Domain.Commands.Client;
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
            services.AddScoped<IRequestHandler<RegisterClientCommand>, ClientCommandHandler>();

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
        }
    }
}
