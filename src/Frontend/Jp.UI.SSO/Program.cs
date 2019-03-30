using System;
using System.Threading.Tasks;
using Jp.UI.SSO.Util;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Jp.UI.SSO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "JP Project - Server SSO";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces)
                .WriteTo.File(@"jpProject_sso_log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 5)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var host = CreateWebHostBuilder(args).Build();

            Task.WaitAll(DbMigrationHelpers.EnsureSeedData(host));

            host.Run(); 
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"))
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog();
                })
                .UseStartup<Startup>();
    }
}
