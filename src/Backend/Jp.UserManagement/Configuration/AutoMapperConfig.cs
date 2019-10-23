using Jp.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jp.Management.Configuration
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
        }
    }
}