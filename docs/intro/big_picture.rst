Overview
========

An overview about current devlopment

.. image:: ../images/bigpicture.JPG

SSO
---

It's responsible for Authenticate users. Check it's credentials and emit Tokens for Applications. 
Authentication is needed when an application needs to know the identity of the current user. Typically these applications manage data on behalf of that user and need to make sure that this user can only access the data for which he is allowed.

Click `here <http://docs.identityserver.io/en/release/intro/big_picture.html#authentication>`_ to check more details.

User Management UI
------------------

A SPA application responsible for create and manager users. Send reset links. E-mail validation. Profile validation, and so on.

Management API
-------------------

API to serve UI's.

Admin UI
--------
It's the Admin user interface to manage identity Server 4.