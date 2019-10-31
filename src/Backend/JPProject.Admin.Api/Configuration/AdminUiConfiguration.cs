using AutoMapper;
using AutoMapper.Configuration;
using JPProject.Admin.Application.AutoMapper;
using JPProject.AspNet.Core;
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
            var database = configuration["ApplicationSettings:DatabaseType"].ToUpper();
            var connString = configuration.GetConnectionString("SSOConnection");
            var identityServerApiBuilder = services.ConfigureJpAdmin<AspNetUser>();

            switch (database)
            {
                case "MYSQL":
                    identityServerApiBuilder.WithMySql<Startup>(connString);
                    break;
                case "SQLSERVER":
                    identityServerApiBuilder.WithSqlServer<Startup>(connString);

                    break;
                case "POSTGRESQL":
                    identityServerApiBuilder.WithPostgreSql<Startup>(connString);
                    break;
                case "SQLITE":
                    identityServerApiBuilder.WithSqlite<Startup>(connString);
                    break;
            }
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
