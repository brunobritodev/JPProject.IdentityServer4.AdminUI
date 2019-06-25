using IdentityServer4.EntityFramework.Mappers;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.Data.Context;
using Jp.UI.SSO.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jp.UI.SSO.Util
{
    public static class DbMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use command bellow:
        /// Nuget package manager: Add-Migration DbInit -context ApplicationIdentityContext -output Data/Migrations
        /// Dotnet CLI: dotnet ef migrations add DbInit -c ApplicationIdentityContext -o Data/Migrations
        /// </summary>
        /// <param name="host"></param>
        public static async Task EnsureSeedData(IWebHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                await EnsureSeedData(services);
            }
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var userContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserIdentityRole>>();

                var id4Context = scope.ServiceProvider.GetRequiredService<JpContext>();
                var storeDb = scope.ServiceProvider.GetRequiredService<EventStoreContext>();

                await WaitForDb(id4Context);
                var tst = id4Context.Database.GetPendingMigrations();
                await id4Context.Database.MigrateAsync();
                await userContext.Database.MigrateAsync();
                await storeDb.Database.MigrateAsync();

                await EnsureSeedIdentityServerData(id4Context, configuration);
                await EnsureSeedIdentityData(userManager, roleManager, configuration);
            }
        }

        /// <summary>
        /// Generate default admin user / role
        /// </summary>
        private static async Task EnsureSeedIdentityData(
            UserManager<UserIdentity> userManager,
            RoleManager<UserIdentityRole> roleManager,
            IConfiguration configuration)
        {
            // Create admin role
            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                var role = new UserIdentityRole { Name = "Administrador" };

                await roleManager.CreateAsync(role);
            }

            // Create admin user
            if (await userManager.FindByNameAsync(Users.GetUser(configuration)) != null) return;

            var user = new UserIdentity
            {
                UserName = Users.GetUser(configuration),
                Email = Users.GetEmail(configuration),
                EmailConfirmed = true,

            };

            var result = await userManager.CreateAsync(user, Users.GetPassword(configuration));

            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim("is4-rights", "manager"));
                await userManager.AddClaimAsync(user, new Claim("username", Users.GetUser(configuration)));
                await userManager.AddClaimAsync(user, new Claim("email", Users.GetEmail(configuration)));
                await userManager.AddToRoleAsync(user, "Administrador");
            }
        }

        /// <summary>
        /// Generate default clients, identity and api resources
        /// </summary>
        private static async Task EnsureSeedIdentityServerData(JpContext context, IConfiguration configuration)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Clients.GetAdminClient(configuration).ToList())
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                var identityResources = ClientResources.GetIdentityResources().ToList();

                foreach (var resource in identityResources)
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in ClientResources.GetApiResources().ToList())
                {
                    await context.ApiResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task WaitForDb(DbContext context)
        {
            var maxAttemps = 12;
            var delay = 5000;

            var healthChecker = new DbHealthChecker();
            for (int i = 0; i < maxAttemps; i++)
            {
                if (healthChecker.TestConnection(context))
                {
                    return;
                }
                await Task.Delay(delay);
            }

            // after a few attemps we give up
            throw new Exception("Error wating database");

        }
    }
}
