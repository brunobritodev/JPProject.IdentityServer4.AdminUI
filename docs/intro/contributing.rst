Contributing
============

.. image:: ../images/logo.png
    :align: center

We are open to community contributions. 

First, Read this: `Being a good open source citizen <https://hackernoon.com/being-a-good-open-source-citizen-9060d0ab9732>`_.
There are a couple of guidelines you should follow so we can handle this without too much effort.

How to contribute?
-------------------

Looking to contribute something to Jp Ploject? **Here's how you can help.**

Contributing to Jp Project
--------------------------

Please take a moment to review this document in order to make the contribution process 
easy and effective for everyone involved.

Following these guidelines helps to communicate that you respect the time of the developers 
managing and developing this open source project. In return, we will be lovely persons 
that respect in addressing your issue or assessing patches and features.

**The easiest way to contribute is to open an issue and start a discussion.**

Using the issue tracker
^^^^^^^^^^^^^^^^^^^^^^^

The `issue tracker <https://github.com/brunohbrito/JP-Project/issues>`_ is the preferred channel for [bug reports](#bug-reports), [features requests](#feature-requests) and [submitting pull requests](#pull-requests), but please respect the following restrictions:

* Please **do not** use the issue tracker for personal support requests.

* Please **do not** post comments consisting solely of "+1" or ":thumbsup:".
  Use `GitHub's "reactions" feature <https://github.com/blog/2119-add-reactions-to-pull-requests-issues-and-comments>`_
  instead.

Bug reports
^^^^^^^^^^^

A bug is a _demonstrable problem_ that is caused by the code in the repository.
Good bug reports are extremely helpful!

Guidelines for bug reports:

0. **Validate and lint your code** &mdash; to ensure your problem isn't caused by a simple error in your own code.

1. **Use the GitHub issue search** &mdash; check if the issue has already been reported.

2. **Check if the issue has been fixed** &mdash; try to reproduce it using the latest `master` or development branch in the repository.

A good bug report shouldn't leave others needing to chase you up for more
information. Please try to be as detailed as possible in your report. What is
your environment? What steps will reproduce the issue? Did you check the logs? All these details will help people to fix
any potential bugs.

Example:

.. pull-quote::

+---------------------------------------------------------------------------------+
|   **Short and descriptive example bug report title**                            |
|                                                                                 |
|   A summary of the issue and the OS environment in which it occurs. If          |
|   suitable, include the steps required to reproduce the bug.                    |
|                                                                                 |
|   1. This is the first step                                                     |
|   2. This is the second step                                                    |
|   3. Further steps, etc.                                                        |
|                                                                                 |
|   Any other information you want to share that is relevant to the issue being   |
|   reported. This might include the lines of code that you have identified as    |
|   causing the bug, and potential solutions (and your opinions on their          |
|   merits).                                                                      |
+------------------------------+--------------------------------------------------+

Feature requests
^^^^^^^^^^^^^^^^

Feature requests are welcome. Before opening a feature request, please take a moment 
to find out whether your idea fits with the scope and aims of the project. It's up 
to *you* to make a strong case to convince the project's developers of the merits of 
this feature. Please provide as much detail and context as possible.

Pull requests
^^^^^^^^^^^^^

**Issue First** 
Before even writing the first line of code raise an issue and get buy in on your proposal 
from the maintainers. There’s several reasons for this, people might already be working
on the issue, the issue might not be an issue or by design, but mainly just letting the 
community know your working on something, it gets “assigned” to you and you get 
implementation detail feedback early. All to reduce chance of redoing work or getting 
your contribution rejected.

Good pull requests—patches, improvements, new features—are a fantastic
help. They should remain focused in scope and avoid containing unrelated
commits.

**Please ask first** before embarking on any significant pull request (e.g.
implementing features, refactoring code, porting to a different language),
otherwise you risk spending a lot of time working on something that the
project's developers might not want to merge into the project.

Adhering to the following process is the best way to get your work
included in the project:

1. `Fork <https://help.github.com/fork-a-repo/>`_ the project, clone your fork,
   and configure the remotes:

.. code::

   # Clone your fork of the repo into the current directory
   git clone https://github.com/<your-username>/JP-Project.git
   # Navigate to the newly cloned directory
   cd free-bootstrap-admin-template
   # Assign the original repo to a remote called "upstream"
   git remote add upstream https://github.com/brunohbrito/JP-Project.git


2. If you cloned a while ago, get the latest changes from upstream:

.. code::

   git checkout master
   git pull upstream master


3. Create a new topic branch (off the main project development branch) to
   contain your feature, change, or fix:

.. code::

   git checkout -b <topic-branch-name>


4. Commit your changes in logical chunks. Please adhere to these `git commit
   message guidelines <http://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html>`_
   or your code is unlikely to be merged into the main project. Use Git's
   `interactive rebase <https://help.github.com/articles/interactive-rebase>`_
   feature to tidy up your commits before making them public.

5. Locally merge (or rebase) the upstream development branch into your topic branch:

.. code::

   git pull [--rebase] upstream master


6. Push your topic branch up to your fork:

.. code::

   git push origin <topic-branch-name>

7. `Open a Pull Request <https://help.github.com/articles/using-pull-requests/>`_ with a clear title and description against the `master` branch.

**IMPORTANT**: By submitting a patch, you agree to allow the project owners to license your work under the terms of the `MIT License <https://github.com/brunohbrito/JP-Project/blob/master/LICENSE>`_.

Platform
^^^^^^^^

Backend of JpProject is built against ASP.NET Core and runs on .NET Framework 4.6.1 (and higher) and .NET Core 2.1 (and higher).

The Frontend SPA is built against Angular 6 and runs on Node and Angular Cli 6.

General feedback and discussions?
---------------------------------

Please start a discussion on the `issue tracker <https://github.com/brunohbrito/JP-Project/issues>`_.
