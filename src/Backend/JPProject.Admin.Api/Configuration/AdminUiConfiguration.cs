using Jp.Database.Context;
using JPProject.AspNet.Core;
using JPProject.Domain.Core.ViewModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace JPProject.Admin.Api.Configuration
{
    public static class AdminUiConfiguration
    {
        public static IServiceCollection ConfigureAdminUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureProviderForContext<EventStoreContext>(DetectDatabase(configuration));
            services.AddDbContext<EventStoreContext>(ProviderSelector.WithProviderAutoSelection(DetectDatabase(configuration)));

            services
                .ConfigureJpAdminServices<AspNetUser>()
                .ConfigureJpAdminStorageServices()
                .AddJpAdminContext(ProviderSelector.WithProviderAutoSelection(DetectDatabase(configuration)))
                .AddEventStore<EventStoreContext>();

            return services;

        }

        public static void ConfigureDefaultSettings(this IServiceCollection services)
        {
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
        }

        /// <summary>
        /// it's just a tuple. Returns 2 parameters.
        /// Trying to improve readability at ConfigureServices
        /// </summary>
        private static (DatabaseType, string) DetectDatabase(IConfiguration configuration) => (
            configuration.GetValue<DatabaseType>("ApplicationSettings:DatabaseType"),
            configuration.GetConnectionString("SSOConnection"));
    }
}
