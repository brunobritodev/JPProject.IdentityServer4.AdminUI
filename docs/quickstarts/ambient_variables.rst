#################
Ambient variables
#################

Using Ambient var you can minimize efforts to change configuration after publish.


SSO Variables
-------------


.. list-table:: Ambient Variables
   :widths: 15 25 25 35
   :header-rows: 1

   * - Variable
     - Default
     - Expected in prod
     - Description
   * - ASPNETCORE_ENVIRONMENT
     - Development
     - Production
     - For more info access `the default docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/environments?view=aspnetcore-2.1>`_
   * - ASPNETCORE_URLS
     - https://+:5000;http://+:5001
     - https://+:443;http://+:80
     - Set the ports for Https and Http. For more info `docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1&tabs=aspnetcore2x>`_ 
   * - APPINSIGHTS_INSTRUMENTATIONKEY
     - <YOUR KEY>
     - Something like 762FAF25-9480-4AF7-8821-06875ED9266C
     - To create an Application Insights on Azure go to `docs <https://docs.microsoft.com/en-us/azure/bot-service/bot-service-resources-app-insights-keys?view=azure-bot-service-3.0>`_
   * - CERTIFICATE_TYPE
     - Temporary
     - File
     - Can be Temporary / File / Store / Environment
   * - ASPNETCORE_Kestrel__Certificates__Default__Path
     - Only used if CERTIFICATE_TYPE is Environment
     - 
     - Ambients like docker or linux it can be usefull
   * - ASPNETCORE_Kestrel__Certificates__Default__Password
     - Only used if CERTIFICATE_TYPE is Environment
     - 
     - Ambients like docker or linux it can be usefull
   * - USER_MANAGEMENT_URI
     - http://localhost:4200
     - 
     - The path Url of User Management UI after published



User Management API Variables
-----------------------------


.. list-table:: Ambient Variables
   :widths: 15 25 25 35
   :header-rows: 1

   * - Variable
     - Default
     - Expected in prod
     - Description
   * - ASPNETCORE_ENVIRONMENT
     - Development
     - Production
     - For more info access `the default docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/environments?view=aspnetcore-2.1>`_
   * - ASPNETCORE_URLS
     - https://+:5000;http://+:5001
     - https://+:443;http://+:80
     - Set the ports for Https and Http. For more info `docs <https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1&tabs=aspnetcore2x>`_ 
   * - APPINSIGHTS_INSTRUMENTATIONKEY
     - <YOUR KEY>
     - Something like 762FAF25-9480-4AF7-8821-06875ED9266C
     - To create an Application Insights on Azure go to `docs <https://docs.microsoft.com/en-us/azure/bot-service/bot-service-resources-app-insights-keys?view=azure-bot-service-3.0>`_
   * - VALIDATE_ISSUER
     - true
     - true
     - In specifics use case, the Issuer URI from User Management UI differs from API. e.g: Docker Ambient
   * - AUTHORITY
     - https://localhost:5000
     - 
     - The path Url of SSO
