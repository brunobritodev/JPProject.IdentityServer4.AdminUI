using System.Threading.Tasks;
using Equinox.SSO.Util;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Equinox.SSO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // Uncomment this to seed upon startup, alternatively pass in `dotnet run / seed` to seed using CLI
            // await DbMigrationHelpers.EnsureSeedData(host);
            // Task.WaitAll(DbMigrationHelpers.EnsureSeedData(host));

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
