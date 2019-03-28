============
App Settings
============

Here you can find detailed explanation about App Settings

There are Ambient Variables, such as Database Connection, that are equivalent in App Settings. In case that both of them are set. The project will respect the Ambient Variables first.

SSO Settings
------------

Connection
^^^^^^^^^^

The project use only one Database and all projects use the same Connection String name **"SSOConnection"**

CertificateOptions
^^^^^^^^^^^^^^^^^^

There are two options to provide CertificateOptions.

* **File** - You need to provide
    * FileName: Entire path of certificate
    * FilePassword: Password of certificate
* **Store** - You need to provide KeyStoreIssuer. The certificate is search by X509FindType.FindByIssuerName. (Good option for Azure SSL)
* **Environment** - Will look into Env Var ASPNETCORE_Kestrel__Certificates__Default__Path and ASPNETCORE_Kestrel__Certificates__Default__Password to locate the certificate and it pass.
* **Temporary** - It will create an auto signed Certificate. Use only on dev's environment.

EmailConfiguration
^^^^^^^^^^^^^^^^^^

E-mail settings to provide the capabilities to send e-mail after a new user and reset link.

ExternalLogin
^^^^^^^^^^^^^

With these settings the SSO can provide External login.

To take token settings from Google, get `docs here <https://developers.google.com/identity/protocols/OAuth2>`_

To take from Facebook, see `docs here <https://developers.facebook.com/docs/facebook-login/access-tokens>`_

ApplicationSettings
^^^^^^^^^^^^^^^^^^^

These settings will be overrided by Environment Variables, if set.

.. list-table:: Ambient Variables
   :widths: 15 25 60
   :header-rows: 1

   * - Variable
     - Value
     - Description
   * - Authority
     - https://localhost:5000
     - The path Url of SSO
   * - UserManagementURL
     - http://localhost:4200
     - The path Url of User Management UI after published
   * - IS4AdminUi
     - http://localhost:4300
     - The path Url of Admin UI
   * - ResourceServerURL
     - https://localhost:5003
     - The path Url of Management API
   * - CUSTOMCONNSTR_DATABASE_CONNECTION
     - <YOUR CONN STRING>
     - Database Connection String
   * - DatabaseType
     - ``MySql`` or ``SqlServer``
     - Which database will be used.


User Management API
-------------------

Connection
^^^^^^^^^^

The project use only one Database and all projects use the same Connection String name **"SSOConnection"**

Storage
^^^^^^^

If user send his profile picture, then the project will upload it to Azure Blob container, so these options must be set.

To get more info how to take Key, see `docs here <https://code.visualstudio.com/tutorials/static-website/create-storage>`_

EmailConfiguration
^^^^^^^^^^^^^^^^^^

E-mail settings to provide the capabilities to send e-mail after a new user and reset link.

ApplicationSettings
^^^^^^^^^^^^^^^^^^^

Need to provide the **DatabaseType**.

This settings will be override by Environment Variables if set.

User Management UI
------------------

environment files
^^^^^^^^^^^^^^^^^

To change settings from UI, you need to open environment file. Located at src/environment.

.. list-table:: UI Variables
   :widths: 30 70
   :header-rows: 1

   * - Variable
     - Description
   * - **GoogleClientId** 
     - Same from ApplicationSettings, the UI use this to take user info while is registering himself.
   * - **FacebookClientId** 
     - Same from ApplicationSettings, the UI use this to take user info while is registering himself.
   * - **ResourceServer** 
     - Base Url from User Management API
   * - **IssuerUri** 
     - Base Url from SSO
   * - **RequireHttps** 
     - True if IsuerUri has a HTTPS.
   * - **Uri**
     - Final URL of App.

Admin UI
------------------

environment files
^^^^^^^^^^^^^^^^^

To change settings from UI, you need to open environment file. Located at src/environment.

.. list-table:: UI Variables
   :widths: 30 70
   :header-rows: 1

   * - Variable
     - Description
   * - **ResourceServer** 
     - Base Url from User Management API
   * - **IssuerUri** 
     - Base Url from SSO
   * - **RequireHttps** 
     - True if IsuerUri has a HTTPS.
   * - **Uri**
     - Final URL of App.