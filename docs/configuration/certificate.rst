Certificate
===========

Self-signed Certificate
-----------------------
To generate certificates, there is a Script ``key\cert.ps1`` that generate Self Signed Certificates.
You can use it to generate certificate and user against docker, or test in your local.

Open up a PowerShell terminal and run the script. It’ll ask you for a certificate name, password, and then save it for you.

Change Config file
^^^^^^^^^^^^^^^^^^

If a certificate is generated, to use it, change the appsettings.json. See `docs <quickstarts/ambient_variables.html>`_ for more info