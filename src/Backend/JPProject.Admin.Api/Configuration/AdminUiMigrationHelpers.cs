using IdentityServer4.EntityFramework.Entities;
using Jp.Database.Context;
using JPProject.Admin.EntityFramework.Repository.Context;
using JPProject.EntityFrameworkCore.MigrationHelper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace JPProject.Admin.Api.Configuration
{
    public static class AdminUiMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use command bellow:
        /// Nuget package manager: Add-Migration DbInit -context EventStoreContext -output Data/Migrations
        /// Dotnet CLI: dotnet ef migrations add DbInit -c EventStoreContext -o Data/Migrations
        /// </summary>
        public static async Task EnsureSeedData(IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;
            var ssoContext = services.GetRequiredService<JpProjectAdminUiContext>();

            Log.Information("Check if database contains Client (ConfigurationDbStore) table");
            await DbHealthChecker.WaitForTable<Client>(ssoContext);

            Log.Information("Check if database contains PersistedGrant (PersistedGrantDbStore) table");
            await DbHealthChecker.WaitForTable<PersistedGrant>(ssoContext);
            Log.Information("Checks done");

            var eventStoreDb = serviceScope.ServiceProvider.GetRequiredService<EventStoreContext>();
            await eventStoreDb.Database.EnsureCreatedAsync();
        }
    }
}
