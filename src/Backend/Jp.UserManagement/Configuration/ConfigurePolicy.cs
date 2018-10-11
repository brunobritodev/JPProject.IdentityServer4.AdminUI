using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Management.Configuration
{
    public static class ConfigurePolicy
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            return;
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IS4-Adm", policy => policy.RequireAssertion(context => context.User.HasClaim("IS4-Permission", "Manager") || context.User.IsInRole("Administrator")).RequireScope("management-api.identityserver4-manager"));
                options.AddPolicy("IS4-ReadOnly", policy => policy.RequireScope("management-api.identityserver4-manager").RequireAssertion(context => context.User.HasClaim("IS4-Permission", "Manager") || context.User.IsInRole("Administrator") || context.User.HasClaim("IS4-Permission", "ReadOnly")));
            });

        }


    }
}
