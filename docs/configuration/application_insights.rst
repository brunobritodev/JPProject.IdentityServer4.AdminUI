Application Insights
====================

The project has the basis config of Application Insights. You can change it at ``Program.cs``. There is a Component of Serilog too.

.. code-block:: csharp

    public static void Main(string[] args)
    {
        ... 
        // Serilog plugin
            .WriteTo.ApplicationInsightsEvents(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"))
        ...
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        // default Application Insights config
            .UseApplicationInsights(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"))
            ...

