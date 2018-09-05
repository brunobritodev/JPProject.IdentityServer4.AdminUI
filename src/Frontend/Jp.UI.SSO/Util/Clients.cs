using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Jp.Infra.CrossCutting.Identity.Constants;

namespace Jp.UI.SSO.Util

{
    public class Clients
    {

        public static IEnumerable<Client> GetAdminClient()
        {

            return new List<Client>
            {
                /*
                 * JP Project ID4 Admin Client
                 */
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
                 * User Management Client - OpenID Connect implicit flow client
                // */
                new Client {
                    ClientId = "UserManagementUI",
                    ClientName = "User Management UI",
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    RedirectUris = { "http://localhost:4200/login-callback" },
                    PostLogoutRedirectUris =  { "http://localhost:4200/" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    LogoUri = "~/images/clientLogo/1.jpg",
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Picture,
                        "UserManagementApi.owner-content",
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
                        "https://localhost:5003/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes =
                    {
                        "UserManagementApi.owner-content"
                    }
                }

            };

        }
    }
}