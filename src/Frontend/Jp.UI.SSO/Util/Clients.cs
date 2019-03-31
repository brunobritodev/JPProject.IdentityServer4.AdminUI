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
                    RedirectUris = { $"{configuration.GetValue<string>("ApplicationSettings:IS4AdminUi")}/login-callback"},
                    AllowedCorsOrigins = { configuration.GetValue<string>("ApplicationSettings:IS4AdminUi")},

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
                    RedirectUris = { $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}/login-callback" },
                    PostLogoutRedirectUris =  { $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}" },
                    AllowedCorsOrigins = { $"{configuration.GetValue<string>("ApplicationSettings:UserManagementURL")}" },
                    LogoUri = "~/images/clientLogo/1.jpg",
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