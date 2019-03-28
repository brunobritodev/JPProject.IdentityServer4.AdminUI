Docker
======

Now you can run through a docker! ❤️

*Unfortunately you need to change hosts for it. Because Authority URL. I can't do anything to face it. It's security feature from OAuth2 to keep same Authority name for each Token.*


.. warning:: 
    - Demo Database.
    - This environment will not persist data once the containers stop running and is only suitable for basic testing.

.. Important:: You must need to update hosts file in order to successful run.

Tutorial
^^^^^^^^^

If you do not already have a working IdentityServer installation up and running, or just want to demo AdminUI, then this walkthrough is for you.

The demo Docker Compose file will run Docker containers for Admin UI, plus containers test IdentityServer installation & database. It will get you up and running quickly with a full demo test environment.

This walk-through will assume you already have Docker installed on your machine. 

The latest Docker compose file can be downloaded `here`_.

.. _here: https://www.identityserver.com/products/

1 - Download
-------------
`Download`_ or `clone`_ project.

.. _Download: https://github.com/brunohbrito/JP-Project/archive/master.zip or 
.. _clone: https://github.com/brunohbrito/JP-Project 

2 - Update HOST
-------------------

Before you begin, add a map within your hosts file from your local IP address to "jpproject".

As follows: ``127.0.0.1 jpproject``

To do so, go to project folder > build(folder) and execute update-hosts.bat as Administrator. Or follow the steps:

* On Windows, your hosts file is usually found at ``C:\Windows\System32\Drivers\etc\hosts``
    * Add ``127.0.0.1 jpproject`` at the end of file
* On Linux, your hosts file is usually found at ``/etc/hosts``
    * Add ``127.0.0.1 jpproject`` at the end of file

3 - Run docker-compose
----------------------

Open terminal (``cmd``) from project folder and type:

.. code-block:: CMD
    :caption: CMD
        
    docker-compose up


BE HAPPY!


Authority vs Issuer name
^^^^^^^^^^^^^^^^^^^^^^^^

Authority name differs from Issuer name inside the docker ambient. The Authority and Issuer url must be the same across the environment. 
Look at the image:

.. image:: ../images/dockerproblem.jpg


If Authority differs from Issure the authorization will fail. There are some ways to do that: 

* Same DNS. Host machine and docker ambient in the same network. So it's possible to access them with the hostname, because they wil have same DNS.
* Change the Api hosts file. Change localhost with the same ip address of SSO.
* Change ID4 validation to ignore Issuer. It's not recomended, but `RFC6749 <https://tools.ietf.org/html/rfc6749>`_ do not have specific rules about it.
