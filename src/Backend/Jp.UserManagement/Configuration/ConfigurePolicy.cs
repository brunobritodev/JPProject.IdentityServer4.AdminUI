using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jp.Application.AutoMapper;
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
                options.AddPolicy("IS4-Adm", policy => policy.RequireAssertion(context => context.User.HasClaim("IS4-Permission", "Manager") || context.User.IsInRole("Administrator")).RequireScope("management-api.identityserver4-manager"));
                options.AddPolicy("IS4-ReadOnly", policy => policy.RequireScope("management-api.identityserver4-manager").RequireClaim("IS4-Permission", "ReadOnly"));
            });

        }


    }
}
