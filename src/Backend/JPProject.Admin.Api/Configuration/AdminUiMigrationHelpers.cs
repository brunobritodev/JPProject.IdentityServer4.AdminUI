using JPProject.Admin.Infra.Data.Context;
using JPProject.EntityFrameworkCore.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace JPProject.Admin.Api.Configuration
{
    public static class AdminUiMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use command bellow:
        /// Nuget package manager: Add-Migration DbInit -context ApplicationIdentityContext -output Data/Migrations
        /// Dotnet CLI: dotnet ef migrations add DbInit -c ApplicationIdentityContext -o Data/Migrations
        /// </summary>
        public static async Task EnsureSeedData(IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;
            await EnsureSeedData(services);
        }
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            await Infra.Data.Configuration.DbMigrationHelpers.CheckDatabases(serviceProvider, new JpDatabaseOptions() { MustThrowExceptionIfDatabaseDontExist = true });

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var eventStoreDb = scope.ServiceProvider.GetRequiredService<EventStoreContext>();
                await Infra.Data.Configuration.DbMigrationHelpers.ConfigureEventStoreContext(eventStoreDb);
            }
        }
    }
}
