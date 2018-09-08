Serilog
=======

To log the project uses Serilog. There are several kinds of Configuration, you can see them at `Serilog homepage <https://serilog.net/>`_

The file ``Program.cs`` of **Jp.UI.SSO** and **Jp.UserManagement** has the configuration of logs.

.. code-block:: csharp

    public static void Main(string[] args)
    {
        ...
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.ApplicationInsightsEvents(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"))
            .WriteTo.File(@"jpProject_sso_log.txt")
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
            .CreateLogger();
        ...
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



