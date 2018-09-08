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

There are two situation where you need to fill CertificateOptions.

* **File** - You need to provide
    * FileName: Entire path of certificate
    * FilePassword: Password of certificate
* **Store** - You need to provide KeyStoreIssuer. The certificate is search by X509FindType.FindByIssuerName.

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

You need to set **UserManagementURL**, so when user cliks on Register link, SSO takes this value to redirect him.

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