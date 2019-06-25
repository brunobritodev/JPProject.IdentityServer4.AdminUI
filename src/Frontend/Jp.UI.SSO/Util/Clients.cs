using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Jp.UI.SSO.Util

{
    public class Clients
    {

        public static IEnumerable<Client> GetAdminClient(IConfiguration configuration)
        {

            return new List<Client>
            {
                /*
                 * JP Project ID4 Admin Client
                 */
                new Client
                {

                    ClientId = "IS4-Admin",
                    ClientName = "IS4-Admin",
                    ClientUri = configuration.GetValue<string>("ApplicationSettings:IS4AdminUi"),
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new[] {
                        $"{configuration.GetValue<string>("ApplicationSettings:IS4AdminUi")}/login-callback",
                        $"{configuration.GetValue<string>("ApplicationSettings:IS4AdminUi")}/silent-refresh.html"
                    },
                    AllowedCorsOrigins = { configuration.GetValue<string>("ApplicationSettings:IS4AdminUi")},
                    IdentityTokenLifetime = 3600,
                    LogoUri = "https://jpproject.azurewebsites.net/sso/images/brand/logo.png",
                    AuthorizationCodeLifetime = 3600,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jp_api.is4"
                    }
                },

                /*
                 * User Management Client - OpenID Connect implicit flow client
                 */
                new Client {
                    ClientId = "UserManagementUI",
                    ClientName = "User Management UI",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    RedirectUris =new[] {
                        $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}/login-callback",
                        $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}/silent-refresh.html"
                    },
                    PostLogoutRedirectUris =  { $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}" },
                    AllowedCorsOrigins = { $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}" },
                    LogoUri = "https://jpproject.azurewebsites.net/sso/images/clientLogo/1.jpg",
                    IdentityTokenLifetime = 3600,
                    AuthorizationCodeLifetime = 3600,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jp_api.user",
                    }
                },
                /*
                 * Swagger
                 */
                new Client
                {
                    ClientId = "Swagger",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    LogoUri = "https://avatars3.githubusercontent.com/u/16343502?s=400&v=4",
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        $"{configuration.GetValue<string>("ApplicationSettings:ResourceServerURL")}/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes =
                    {
                        "jp_api.user",
                        "jp_api.is4",
                    }
                }

            };

        }
    }
}