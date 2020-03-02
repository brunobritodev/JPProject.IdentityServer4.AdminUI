using JPProject.Admin.Api.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace JPProject.Admin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "JP Project - Admin Light API";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces)
                .WriteTo.File(@"jpProject_sso_log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 5)
                .WriteTo.Console()
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            AdminUiMigrationHelpers.EnsureSeedData(host.Services.CreateScope()).Wait();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddSerilog();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
