using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Jp.Infra.CrossCutting.Identity.Constants;
using Jp.Infra.CrossCutting.Tools.DefaultConfig;

namespace Jp.UI.SSO.Util

{
    public class Clients
    {

        public static IEnumerable<Client> GetAdminClient()
        {

            return new List<Client>
            {
                new Client
                {

                    ClientId = AuthorizationConsts.OidcClientId,
                    ClientName = AuthorizationConsts.OidcClientId,
                    ClientUri = AuthorizationConsts.IdentityAdminBaseUrl,

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{AuthorizationConsts.IdentityAdminBaseUrl}/signin-oidc"},
                    FrontChannelLogoutUri = $"{AuthorizationConsts.IdentityAdminBaseUrl}/signout-oidc",
                    PostLogoutRedirectUris = { $"{AuthorizationConsts.IdentityAdminBaseUrl}/signout-callback-oidc"},
                    AllowedCorsOrigins = { AuthorizationConsts.IdentityAdminBaseUrl },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    }
                },
                /*
                 * JP Project ID4 Admin Client
                 */
                new Client
                {

                    ClientId = "IS4-Admin",
                    ClientName = "IS4-Admin",
                    ClientUri = JpProjectConfiguration.IdentityServerAdminUrl,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{JpProjectConfiguration.IdentityServerAdminUrl}/login-callback"},
                    AllowedCorsOrigins = { JpProjectConfiguration.IdentityServerAdminUrl},

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
                // */
                new Client {
                    ClientId = "UserManagementUI",
                    ClientName = "User Management UI",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    RedirectUris = { $"{JpProjectConfiguration.UserManagementUrl}/login-callback" },
                    PostLogoutRedirectUris =  { $"{JpProjectConfiguration.UserManagementUrl}" },
                    AllowedCorsOrigins = { $"{JpProjectConfiguration.UserManagementUrl}" },
                    LogoUri = "~/images/clientLogo/1.jpg",
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jp_api.user",
                    }
                },
                new Client
                {
                    ClientId = "Swagger",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        $"{JpProjectConfiguration.ResourceServer}/swagger/oauth2-redirect.html"
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