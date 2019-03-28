#################
Ambient variables
#################

Using Ambient var you can minimize efforts to change configuration after publish.


SSO Variables
-------------


.. list-table:: Ambient Variables
   :widths: 10 20 20 20 30
   :header-rows: 1

   * - Variable
     - Default
     - Docker
     - Expected in prod
     - Description
   * - ASPNETCORE_ENVIRONMENT
     - Devlopment
     - Devlopment
     - Production
     - For more info access `the default docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/environments?view=aspnetcore-2.2>`_
   * - ASPNETCORE_URLS
     - https://+:5001;http://+:5000
     - http://+:5000
     - https://+:443;http://+:80
     - Set the ports for Https and Http. For more info `docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1&tabs=aspnetcore2x>`_ 
   * - APPINSIGHTS_INSTRUMENTATIONKEY
     - <YOUR KEY>
     - <YOUR KEY>
     - Something like 762FAF25-9480-4AF7-8821-06875ED9266C
     - To create an Application Insights on Azure go to `docs <https://docs.microsoft.com/en-us/azure/bot-service/bot-service-resources-app-insights-keys?view=azure-bot-service-3.0>`_
   * - CERTIFICATE_TYPE
     - Temporary
     - Temporary
     - File
     - Can be Temporary / File / Store / Environment
   * - USER_MANAGEMENT_URI
     - http://localhost:4200
     - http://localhost:4200
     - 
     - Url of User Management UI after published
   * - IS4_ADMIN_UI
     - http://localhost:4300
     - http://localhost:4300
     - 
     - The path Url of Admin UI
   * - RESOURCE_SERVER_URI
     - https://localhost:5002
     - http://localhost:5003
     - 
     - The path Url of Management API
   * - CUSTOMCONNSTR_DATABASE_CONNECTION
     - <YOUR CONN STRING>
     - check at docker.compose
     - 
     - Database Connection String Specially made for Azure App Service
   * - DATABASE_TYPE
     - ``MySql`` or ``SqlServer``
     - ``MySql``
     - 
     - Which database will be used.



User Management API Variables
-----------------------------


.. list-table:: Ambient Variables
   :widths: 10 20 20 20 30
   :header-rows: 1

   * - Variable
     - Default
     - Docker
     - Expected in prod
     - Description
   * - ASPNETCORE_ENVIRONMENT
     - Devlopment
     - Devlopment
     - Production
     - For more info access `the default docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/environments?view=aspnetcore-2.2>`_
   * - ASPNETCORE_URLS
     - https://+:5002;http://+:5003
     - http://+:5003
     - https://+:443;http://+:80
     - Set the ports for Https and Http. For more info `docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1&tabs=aspnetcore2x>`_ 
   * - APPINSIGHTS_INSTRUMENTATIONKEY
     - <YOUR KEY>
     - <YOUR KEY>
     - Something like 762FAF25-9480-4AF7-8821-06875ED9266C
     - To create an Application Insights on Azure go to `docs <https://docs.microsoft.com/en-us/azure/bot-service/bot-service-resources-app-insights-keys?view=azure-bot-service-3.0>`_
   * - AUTHORITY
     - https://localhost:5001
     - http://jpproject:5000
     - 
     - Authority URL
   * - CUSTOMCONNSTR_DATABASE_CONNECTION
     - <YOUR CONN STRING>
     - check at docker.compose
     - 
     - Database Connection String Specially made for Azure App Service
   * - DATABASE_TYPE
     - ``MySql`` or ``SqlServer``
     - ``MySql``
     - 
     - Which database will be used.

     