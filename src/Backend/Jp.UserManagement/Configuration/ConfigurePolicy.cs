using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Management.Configuration
{
    public static class ConfigurePolicy
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", 
                    policy => policy.RequireAssertion(c => 
                        c.User.HasClaim("is4-rights", "manager") || 
                        c.User.IsInRole("Administrador")));

                options.AddPolicy("ReadOnly", policy =>
                    policy.RequireAssertion(c => 
                        c.User.IsAuthenticated() || 
                        c.User.HasClaim("is4-rights", "manager") || 
                        c.User.IsInRole("Administrador")));

                options.AddPolicy("UserManagement", policy =>
                    policy.RequireAuthenticatedUser());
            });

        }
    }
}
