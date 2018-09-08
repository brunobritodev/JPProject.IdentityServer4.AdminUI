=========
First Use
=========

In this section you will learn the basics to Get Ready!

Pre-requisites
--------------

To build solution you need to certify about these components first

* `.NET Core 2.1 <https://www.microsoft.com/net/download>`_
* `node 8 <https://nodejs.org/en/>`_
* npm 5
* `Angular CLI 6.1 <https://github.com/angular/angular-cli/wiki>`_


Build Files
-----------

After |download| `here <https://github.com/brunohbrito/JP-Project/archive/master.zip>`_ or `clone <https://github.com/brunohbrito/JP-Project>`_ the initial state of project is:

* Use Sql Server LocalDb
* Temporary Certificate
* Auto Migration enabled

Using build.bat
^^^^^^^^^^^^^^^

Open folder *build* and execute build.bat. The file will install Nuget and NPM dependencies. Then compile and run.

Using build.ps1
^^^^^^^^^^^^^^^

Sometimes there are missing parameters at Environment Path, so the build.bat can't build. 

Open powershell as Admin. Navigate to build folder. Execute these commands:

* Set-ExecutionPolicy Unrestricted
* .\build.ps1 
* Set-ExecutionPolicy AllSigned

.. raw:: html

    <div style="position: relative; height: 0; overflow: hidden; max-width: 100%; height: auto;">
        <iframe src="https://player.vimeo.com/video/288753436?color=ff9933&title=0&byline=0" width="800" height="480" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
    </div>


Using VS and VSCode
--------------------

The default way to Start the project.

SSO and API
^^^^^^^^^^^
To load project open src/JpProject.sln vith Visual Studio. Now you need to set Multiple Startup Projects.

.. image:: ../images/multiple-startup-project.png

Run the project

User Management
^^^^^^^^^^^^^^^

Open VSCode then go to File > Open Folder > Locate src\Frontend\Jp.UserManagement.

Open Terminal :kbd:`CTRL` + :kbd:`'`. Type: 

* npm install
* ng serve

Wait and open Browser at http://localhost:4200

.. raw:: html

    <div style="position: relative; height: 0; overflow: hidden; max-width: 100%; height: auto;">
        <iframe src="https://player.vimeo.com/video/288762840?color=ff9933&title=0&byline=0" width="800" height="600" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
    </div>