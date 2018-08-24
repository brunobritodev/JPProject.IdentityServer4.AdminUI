using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Application.Interfaces;
using Equinox.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    internal class ApplicationBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IUserManagerAppService, UserManagerAppService>();
        }
    }
}
