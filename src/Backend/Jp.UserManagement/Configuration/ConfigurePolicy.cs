using IdentityServer4.Extensions;
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
                        c.User.IsInRole("Administrator")));

                options.AddPolicy("ReadOnly", policy =>
                    policy.RequireAssertion(context => context.User.IsAuthenticated()));

                options.AddPolicy("UserManagement", policy =>
                    policy.RequireAuthenticatedUser());
            });

        }
    }
}
