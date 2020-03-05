![image](https://github.com/brunohbrito/JPProject.Core/blob/master/build/logo.png?raw=true)


[![Build Status](https://dev.azure.com/brunohbrito/Jp%20Project/_apis/build/status/JPProject%20AdminUI%20-%20CD?branchName=master)](https://dev.azure.com/brunohbrito/Jp%20Project/_build/latest?definitionId=2&branchName=master)
[![License](https://img.shields.io/github/license/brunohbrito/JPProject.IdentityServer4.AdminUI)](LICENSE)

This is an Administrator Panel for IdentityServer4. It's available in 2 versions: Light and Full. See below the differences.

# Several break changes

The new version, written in ASP.NET Core 3.0, changed a lot. So before upgrade read differences between light and full version. If have the past version, don't worry. It's the same project at all. But splited repo's.

# Installation

If you know the differences between Light and Full. Check the installation instructions below. If wanna understand, check more here at[Presentation](#presentation) section.

## Full Install

Go to this [repo](https://github.com/brunohbrito/JPProject.IdentityServer4.SSO) and follow instructions there.

## Light Install

You will need to create a Client and API resources in your IdentityServer4. At the end of this section there are some shortcuts.

1. [Download](https://github.com/brunohbrito/JPProject.IdentityServer4.AdminUI/archive/master.zip)/Clone or [Fork](https://github.com/brunohbrito/JPProject.IdentityServer4.AdminUI/fork) this repository.
2. Open `environment.ts` and change settings for you SSO.
    ```
    export const environment = {
        production: false,
        IssuerUri:  "http://localhost:5000",
        ResourceServer: "http://localhost:5002/",
        RequireHttps: false,
        Uri: "http://localhost:4300",
        defaultTheme: "E",
        version: "3.0.0"
    };
    ```
    For more details check [angular-oauth2-oidc](https://github.com/manfredsteyer/angular-oauth2-oidc)
3. Open `docker-compose.yml` and change Api Settings:
    ```
    # #############################
    # # Management API - Light
    # #############################
    jpproject-light-api:
        image: jpproject-light-api
        build: 
          context: .
          dockerfile: api-light.dockerfile
        environment: 
            ASPNETCORE_ENVIRONMENT: "Development"
            CUSTOMCONNSTR_SSOConnection: "<you database connstring>"
            ApplicationSettings:Authority: "<you sso uri>"
            ApplicationSettings:DatabaseType: SqlServer # Choose one: SqlServer | MySql | Postgre | Sqlite
            ApplicationSettings:RequireHttpsMetadata: 'false'
            ApplicationSettings:ApiName: "<api name>"
            ApplicationSettings:ApiSecret: "<your api secret>"
    ```
4. Build compose by: `docker-compose up`

Shortcuts:

You must have these 2 configurations at you IdentityServer4

Client configuration
```
    /*
    * JP Project ID4 Admin Client
    */
    new Client
    {

        ClientId = "IS4-Admin",
        ClientName = "IS4-Admin",
        ClientUri = "http://localhost:4300",
        AllowedGrantTypes = GrantTypes.Code,
        AllowAccessTokensViaBrowser = false,
        RequireConsent = true,
        RequirePkce = true,
        AllowPlainTextPkce = false,
        RequireClientSecret = false,
        RedirectUris = new[] {
            "http://localhost:4300/login-callback",
            "http://localhost:4300/silent-refresh.html"
        },
        AllowedCorsOrigins = { "http://localhost:4300" },
        LogoUri = "https://jpproject.azurewebsites.net/sso/images/brand/logo.png",
        PostLogoutRedirectUris = {"http://localhost:4300",},
        AllowedScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            "jp_api.is4",
            "role"
        }
    },

```

Api resource configuration
```
    new ApiResource
    {
        Name = "jp_api",
        DisplayName = "JP API",
        Description = "OAuth2 Server Management Api",
        ApiSecrets = { new Secret(":}sFUz}Pjc]K4yiW>vDjM,+:tq=U989dxw=Vy*ViKrP+bjNbWC3B3&kE23Z=%#Jr".Sha256()) },

        UserClaims =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            "is4-rights",
            "username",
            "roles"
        },

        Scopes =
        {
            new Scope()
            {
                Name = "jp_api.is4",
                DisplayName = "OAuth2 Server",
                Description = "Manage mode to IS4",
                Required = true
            }
        }
    }
```

Identity Resource:
```
    new IdentityResource("role", new List<string>(){"roles"}),
```


## Table of Contents ##

- [Several break changes](#several-break-changes)
- [Installation](#installation)
  - [Full Install](#full-install)
  - [Light Install](#light-install)
  - [Table of Contents](#table-of-contents)
- [Presentation](#presentation)
  - [Full](#full)
  - [Light version](#light-version)
  - [Admin UI](#admin-ui)
- [Demo](#demo)
  - [We are online](#we-are-online)
- [Technologies](#technologies)
  - [Architecture](#architecture)
  - [Give a Star! ⭐](#give-a-star-%e2%ad%90)
  - [How to build](#how-to-build)
- [Docs](#docs)
  - [Contributors](#contributors)
  - [Contributing](#contributing)
  - [Free](#free)
  - [3.0.2](#302)
  - [3.0.1](#301)
  - [v1.4.5](#v145)
- [What comes next?](#what-comes-next)
- [License](#license)

------------------

# Presentation

JP Project Admin Panel is an administrative panel for IdentityServer4. You can manage Clients, Api Resources, Identity Resources and so on. There are 2 versions.

## Full

The full version is for those who don't have an IdentityServer up and running. So you can download the JP Project SSO and with this admin panel you will be able to manage **Users** and **IdentityServer4**.

Go to this [repo](https://github.com/brunohbrito/JPProject.IdentityServer4.SSO) instead

## Light version

For those who already have an IdentityServer4. This panel has features to manage an existing **IdentityServer4** database.

Here some screenshots

## Admin UI ##
<img src="https://github.com/brunohbrito/JPProject.IdentityServer4.AdminUI/blob/master/build/jp-adminui.gif"  width="480" />

# Demo #

Check our full demo online.

## We are online

<img align="right" width="100px" src="https://www.developpez.net/forums/attachments/p289604d1/a/a/a" />

Check it now at [Admin Panel](https://admin.jpproject.net).

You can check also [SSO](https://sso.jpproject.net) and [Profile Manager](https://user.jpproject.net)

_New users are readonly_

# Technologies #

Check below how it was developed.

Written in ASP.NET Core and Angular 8.
The main goal of project is to be a Management Ecosystem for IdentityServer4. Helping Startup's and Organization to Speed Up the Setup of User Management. Helping teams and entrepreneurs to achieve the company's primary purpose: Maximize shareholder value.

- Angular 8
- Rich UI interface
- ASP.NET Core 3.0
- ASP.NET WebApi Core
- MySql Ready
- Sql Ready
- Postgree Ready
- SQLite Ready
- Entity Framework Core
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
Jp Project is built against ASP.NET Core 3.0.

* [Install](https://www.microsoft.com/net/download/core#/current) the latest .NET Core 2.2 SDK

`src/JpProject.AdminUi.sln` Contains the API

For UI's use VSCode.
- AdminUI -> Inside VSCode open folder `rootFolder/src/Frontend/Jp.AdminUI`, then terminal and `npm install && npm start`

Wait for ng to complete his proccess then go to http://localhost:4300!

Any doubts? Go to docs

# Docs #

Wanna start? please [Read the docs](https://jp-project.readthedocs.io/en/latest/index.html)

## Contributors

Thank you all!

[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/0)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/0)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/1)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/1)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/2)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/2)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/3)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/3)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/4)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/4)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/5)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/5)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/6)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/6)[![](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/images/7)](https://sourcerer.io/fame/brunohbrito/brunohbrito/JPProject.IdentityServer4.AdminUI/links/7)

## Contributing

We'll love it! Please [Read the docs](https://jp-project.readthedocs.io/en/latest/index.html)

## Free ##

If you need help building or running your Jp Project platform
There are several ways we can help you out.

## 3.0.2

1. Menu translation
2. Email support for full version
3. Email configuration settings (SMTP / Password) for full version

## 3.0.1

1. ASP.NET Core 3.0 support
2. Separated repositories, for better management. Improving tests, integration tests. And to support more scenarios.

## v1.4.5

Breaking change: **Argon2 password hashing**. Be careful before update. If you are using the old version all users must need to update their passwords.

1. Bug fixes:
   1. Tooltip for admin-ui
2. Argon2 Password Hasher
3. Show version at footer

Check [Changelog.md](https://github.com/brunohbrito/JPProject.IdentityServer4.AdminUI/blob/master/CHANGELOG.md) for a complete list of changes.

# What comes next?

* Code coverage
* UI for Device codes 
* CI with SonarCloud
* E-mail template management
* Blob service management

# License

Jp Project is Open Source software and is released under the MIT license. This license allow the use of Jp Project in free and commercial applications and libraries without restrictions.
