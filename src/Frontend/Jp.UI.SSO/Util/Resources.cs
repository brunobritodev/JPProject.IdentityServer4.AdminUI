using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

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

                // custom identity resource with some consolidated claims
                new IdentityResource("picture", new[] { JwtClaimTypes.Picture }),
                new IdentityResource("management-api-permissions", new[] { JwtClaimTypes.Role }),
                // add additional identity resource
                new IdentityResource("IS4-Permission", "IdentityServer4 Admin Panel Permissions", new [] { "is4-permissions"})

            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                                {
                                    Name = "management-api",
                                    DisplayName = "User Management API",
                                    Description = "API with default and protected actions to register and manager User",
                                    ApiSecrets = { new Secret("Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++".Sha256()) },

                                    UserClaims =
                                    {
                                        IdentityServerConstants.StandardScopes.OpenId,
                                        IdentityServerConstants.StandardScopes.Profile,
                                        IdentityServerConstants.StandardScopes.Email,
                                        "is4-permissions"
                                    },

                                    Scopes =
                                    {
                                        new Scope()
                                        {
                                            Name = "management-api.owner-content",
                                            DisplayName = "User Management - Full access",
                                            Description = "Full access to User Management",
                                            Required = true
                                        },
                                        new Scope()
                                        {
                                            Name = "management-api.identityserver4-manager",
                                            DisplayName = "Administrator",
                                            Description = "Manage mode to IS4",
                                            Required = true
                                        }
                                    }
                                }
                        };
        }
    }
}