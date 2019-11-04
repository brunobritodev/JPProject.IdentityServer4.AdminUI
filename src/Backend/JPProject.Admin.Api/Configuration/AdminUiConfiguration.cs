using AutoMapper;
using AutoMapper.Configuration;
using JPProject.Admin.Application.AutoMapper;
using JPProject.Admin.Database;
using JPProject.AspNet.Core;
using JPProject.Domain.Core.ViewModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace JPProject.Admin.Api.Configuration
{
    public static class AdminUiConfiguration
    {
        public static IServiceCollection ConfigureAdminUi(this IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration.GetValue<DatabaseType>("ApplicationSettings:DatabaseType");
            var connString = configuration.GetConnectionString("SSOConnection");

            services.ConfigureJpAdmin<AspNetUser>().AddDatabase(database, connString);

            return services;

        }

        public static void ConfigureDefaultSettings(this IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();
            AdminUiMapperConfiguration.RegisterMappings().ForEach(p => configurationExpression.AddProfile(p));
            var automapperConfig = new MapperConfiguration(configurationExpression);

            services.TryAddSingleton(automapperConfig.CreateMapper());
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
        }
    }
}
