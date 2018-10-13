using Jp.Application.Interfaces;
using Jp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class ApplicationBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPersistedGrantAppService, PersistedGrantAppService>();
            services.AddScoped<IApiResourceAppService, ApiResourceAppService>();
            services.AddScoped<IIdentityResourceAppService, IdentityResourceAppService>();
            services.AddScoped<IScopesAppService, ScopesAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserManageAppService, UserManagerAppService>();
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<IRoleManagerAppService, RoleManagerAppService>();
        }
    }
}
