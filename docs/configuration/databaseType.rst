Database Type
=============

By default the project come with MySql, to change is very simple. You just need to change the ``appsettings.json``.

The file ``appsettings.json`` of **Jp.UI.SSO** and **Jp.UserManagement** has a DatabaseType property. Change it to ``MySql`` or ``SqlServer``

.. warning:: Both of them must point to the same config Database. You just need to config **Connection String** at Environment or at ``appsettings.json``. For Environment see `docs here <quickstarts/ambient_variables.html>`_


SQL on Docker
-------------

Don't have the SQL Server on you local machine? Use it from Docker

.. code::

   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=@Password1' -e 'MSSQL_PID=Express' -p 1433:1433 -d microsoft/mssql-server-linux:latest

As simple as that!