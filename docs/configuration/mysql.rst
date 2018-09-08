MySql Support
=============

By default the project become with SQL Server, to change is very simple. You just need to change the ``Startup.cs``.

The file ``Startup.cs`` of **Jp.UI.SSO** and **Jp.UserManagement** has the config of Database. Change to this:

.. code-block:: csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        // Configure identity
        services.AddIdentityMySql(Configuration);
        ...
        // Configure identity server
        services.AddIdentityServer(Configuration, _environment, _logger).UseIdentityServerMySqlDatabase(services, Configuration, _logger);
        ...
    }

Both of them must point to the same config Database. You just need to config **Connection String** at Environment or at ``appsettings.json``. For Environment see `docs here <quickstarts/ambient_variables.html>`_

