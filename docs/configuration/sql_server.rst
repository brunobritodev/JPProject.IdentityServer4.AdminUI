SQL Server
==========

By default the project become with Migration ready and the configs set to SQL Server.

The file ``Startup.cs`` of **Jp.UI.SSO** and **Jp.UserManagement** has the config of Database.

.. code-block:: csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        // Configure identity
        services.AddIdentitySqlServer(Configuration);
        ...
        // Configure identity server
        services.AddIdentityServer(Configuration, _environment, _logger).UseIdentityServerSqlDatabase(services, Configuration, _logger);
        ...
    }

Both of them must point to the same config Database. You just need to config **Connection String** at Environment or at ``appsettings.json``. For Environment see `docs here <quickstarts/ambient_variables.html>`_

