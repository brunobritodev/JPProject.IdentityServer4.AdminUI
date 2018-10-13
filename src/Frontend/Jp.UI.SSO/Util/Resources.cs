using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Jp.UI.SSO.Util
{
    public class ClientResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("username", new []{ "username"}),
                // custom identity resource with some consolidated claims
                new IdentityResource("roles", "Roles", new[] { JwtClaimTypes.Role }),

                // add additional identity resource
                new IdentityResource("is4-rights", "IdentityServer4 Admin Panel Permissions", new [] { "is4-rights"})

            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                                {
                                    Name = "jp_api",
                                    DisplayName = "JP API",
                                    Description = "OAuth2 Server Management Api",
                                    ApiSecrets = { new Secret("Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++".Sha256()) },

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
                                            Name = "jp_api.user",
                                            DisplayName = "User Management - Full access",
                                            Description = "Full access to User Management",
                                            Required = true
                                        },
                                        new Scope()
                                        {
                                            Name = "jp_api.is4",
                                            DisplayName = "OAuth2 Server",
                                            Description = "Manage mode to IS4",
                                            Required = true
                                        }
                                    }
                                }
                        };
        }
    }
}