Docker
======

In root project there are 3 dockerfiles. They are functional, but compose file doesn't work.

docker-compose.yml
------------------

There are 2 problems to acomplish that.


SSL bug
^^^^^^^
There is a bug in SSL with .netcore 2.1 in Docker. There is a open `Issue <https://github.com/dotnet/corefx/issues/31034>`_ in dotnet.

Authority vs Issuer name
^^^^^^^^^^^^^^^^^^^^^^^^

Authority name differs from Issuer name inside the docker ambient. The Authority and Issuer url must be the same all across the ambient. 
Look at the image:

.. image:: ../images/dockerproblem.jpg


If Authority differs from Issure the authorization will fail. There are some ways to do that: 

* Same DNS. Host machine and docker ambient in the same network. So it's possible to access them with the hostname, because they wil have same DNS.
* Change the Api hosts file. Change localhost with the same ip address of SSO.
* Change ID4 validation to ignore Issuer. It's not recomended, but `RFC6749 <https://tools.ietf.org/html/rfc6749>`_ do not have specific rules about it.

Because SSL bug, I'm waiting to play with this again.