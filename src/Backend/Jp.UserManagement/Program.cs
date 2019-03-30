using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace Jp.Management
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.Title = "JP Project - User Management";

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

            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"))
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
