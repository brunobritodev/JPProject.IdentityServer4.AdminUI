using AutoMapper;
using JPProject.Admin.Application.AutoMapper;
using JPProject.Admin.Infra.Data.Sqlite.Configuration;
using JPProject.AspNet.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JPProject.Admin.Api.Configuration
{
    public static class AdminUiConfiguration
    {
        public static void ConfigureAdminUi(this IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration["ApplicationSettings:DatabaseType"].ToUpper();
            var connString = configuration.GetConnectionString("SSOConnection");
            services.ConfigureJpAdmin<AspNetUser>();
            switch (database)
            {
                case "MYSQL":
                    services.WithMySql(connString);
                    break;
                case "SQLSERVER":
                    services.WithSqlServer(connString);
                    break;
                case "POSTGRESQL":
                    services.WithPostgreSql(connString);
                    break;
                case "SQLITE":
                    services.WithSqlite(connString);
                    break;
            }

            var mappings = AdminUiMapperConfiguration.RegisterMappings();
            var automapperConfig = new MapperConfiguration(mappings);
            services.TryAddSingleton(automapperConfig.CreateMapper());
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));


        }
    }
}
