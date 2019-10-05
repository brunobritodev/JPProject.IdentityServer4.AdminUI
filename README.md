![image](https://github.com/brunohbrito/JP-Project/blob/master/docs/images/logo.png?raw=true)

[![Build status](https://ci.appveyor.com/api/projects/status/08v6mg6q439x16xt?svg=true)](https://ci.appveyor.com/project/brunohbrito/jp-project)
[![Build Status](https://dev.azure.com/brunohbrito/JpProject/_apis/build/status/JPProject%20CD%20Build?branchName=master)](https://dev.azure.com/brunohbrito/JpProject/_build/latest?definitionId=2&branchName=master)
[![License](https://img.shields.io/github/license/brunohbrito/jp-project.svg)](LICENSE) [![Greenkeeper badge](https://badges.greenkeeper.io/brunohbrito/JPProject.IdentityServer4.AdminUI.svg)](https://greenkeeper.io/)
![DOCS](https://readthedocs.org/projects/jp-project/badge/?version=latest&style=flat)

Jp Project is a Open Source UI Administration Tools for IdentityServer4 v2 - release 2.4.0. 


## Table of Contents ##

- [Presentation](#presentation)
  - [Admin UI](#admin-ui)
  - [Login page](#login-page)
  - [Consent page](#consent-page)
  - [Profile](#profile)
- [Demo](#demo)
  - [We are online at Azure.](#we-are-online-at-azure)
- [Docker](#docker)
- [Technologies](#technologies)
  - [Architecture](#architecture)
  - [Give a Star! ⭐](#give-a-star-%e2%ad%90)
  - [How to build](#how-to-build)
- [Docs](#docs)
  - [Contributing](#contributing)
  - [Free](#free)
  - [v1.4.5](#v145)
  - [v1.4.0](#v140)
  - [v1.3](#v13)
  - [v1.2](#v12)
- [What comes next?](#what-comes-next)
- [License](#license)

------------------

# Presentation

Here some screenshots

## Admin UI ##
<img src="https://github.com/brunohbrito/JP-Project/blob/master/docs/images/jp-adminui.gif"  width="480" />

## Login page ##
<img src="https://github.com/brunohbrito/JP-Project/blob/master/docs/images/login.JPG?raw=true" width="480" />

## Consent page ##
<img src="https://github.com/brunohbrito/JP-Project/blob/master/docs/images/consent-page.JPG?raw=true" width="480" />

## Profile ##
<img src="https://github.com/brunohbrito/JP-Project/blob/master/docs/images/jp-usermanagement.gif" width="480" />

# Demo #

Check our demo online.

## We are online at Azure. 

<img align="right" width="100px" src="https://www.developpez.net/forums/attachments/p289604d1/a/a/a" />

Check it now at [Jp Project](https://jpproject.azurewebsites.net/admin-ui/).

You can check also [SSO](https://jpproject.azurewebsites.net/sso/) and [User Management](https://jpproject.azurewebsites.net/user-management/)

_New users are readonly_

# Docker #

Run through docker compose ❤️

Wanna try? As easy as:

Windows users:
* download [jpproject-docker-windows.zip](https://github.com/brunohbrito/JP-Project/raw/master/build/jpproject-docker-windows.zip)
* Unzip and execute `docker-run.bat` (As administrator)

Linux users:
* Download [docker-compose.yml](https://github.com/brunohbrito/JP-Project/raw/master/build/docker-compose.yml)
* Add `127.0.0.1 jpproject` entry to hosts file (`/etc/hosts`)
* `docker-compose up`


# Technologies #

Check below how it was developed.

Written in ASP.NET Core and Angular 8.
The main goal of project is to be a Management Ecosystem for IdentityServer4. Helping Startup's and Organization to Speed Up the Setup of User Management. Helping teams and entrepreneurs to achieve the company's primary purpose: Maximize shareholder value.

- Angular 8
- Rich UI interface
- ASP.NET Core 2.2
- ASP.NET MVC Core 
- ASP.NET WebApi Core
- ASP.NET Identity Core
- Argon2 Password Hashing
- MySql Ready
- Sql Ready
- Postgree Ready
- SQLite Ready
- Entity Framework Core 2.2
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- High customizable
- Translation for 7 different languages


## Architecture

- Architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work
- Repository and Generic Repository

## Give a Star! ⭐

Do you love it? give us a Star!

## How to build
Jp Project is built against ASP.NET Core 2.2.

* [Install](https://www.microsoft.com/net/download/core#/current) the latest .NET Core 2.2 SDK


`src/JpProject.sln` Contains SSO and API

For UI's use VSCode.
- AdminUI -> Inside VSCode open folder `rootFolder/src/Frontend/Jp.AdminUI`, then terminal and `npm install && npm start`
- User Management -> Inside VSCode open folder `rootFolder/src/Frontend/Jp.UserManagement`, then terminal and `npm install && npm start`

Wait for ng to complete his proccess then go to http://localhost:5000!

Any doubts? Go to docs

# Docs #

Wanna start? please [Read the docs](https://jp-project.readthedocs.io/en/latest/index.html)

## Contributing

We'll love it! Please [Read the docs](https://jp-project.readthedocs.io/en/latest/index.html)

## Free ##

If you need help building or running your Jp Project platform
There are several ways we can help you out.

## v1.4.5

Breaking change: **Argon2 password hashing**. Be careful before update. If you are using the old version all users must need to update their passwords.

1. Bug fixes:
   1. Tooltip for admin-ui
2. Argon2 Password Hasher
3. Show version at footer

## v1.4.0

1. Added :boom: **New Translations** (auto-generate) :green_heart: :blue_heart:
   * Spanish
   * French
   * Dutch
   * Russian
   * Chinese Simplified
   * Chinese Traditional

    <small>If you find some mistakes feel free to PR</small>

2. Added integration with Azure DevOps for full CI/CD. ASAP SonarQube

3. Bug fixes

## v1.3

- Bug fixes
  - angular-oauth2-oidc Session Improvements for Angular Apps. Incluind Admin UI
  - Some Action attributes was HttpPost instead HttpPut (fixed)
- New unity tests

## v1.2

- Docker support
- Available at Docker Hub
- IdentityServer4 v2 (release 2.4.0)
  - Device flow
- ASP.NET Core 2.2 support
- Plugins update
- Angular 7.2

Check [Changelog.md](https://github.com/brunohbrito/JP-Project/blob/master/CHANGELOG.md) for a complete list of changes.

# What comes next?

* Code coverage
* UI for Device codes 
* CI with SonarCloud


# License

Jp Project is Open Source software and is released under the MIT license. This license allow the use of Jp Project in free and commercial applications and libraries without restrictions.
