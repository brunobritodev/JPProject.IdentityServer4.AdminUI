using Jp.Application.Interfaces;
using Jp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Application.Configuration
{
    internal class ApplicationBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPersistedGrantAppService, PersistedGrantAppService>();
            services.AddScoped<IApiResourceAppService, ApiResourceAppService>();
            services.AddScoped<IIdentityResourceAppService, IdentityResourceAppService>();
            services.AddScoped<IScopesAppService, ScopesAppService>();
            services.AddScoped<IClientAppService, ClientAppService>();
        }
    }
}
