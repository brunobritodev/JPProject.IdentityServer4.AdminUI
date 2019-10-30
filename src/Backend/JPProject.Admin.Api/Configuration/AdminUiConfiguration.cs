using AutoMapper;
using JPProject.Admin.Application.AutoMapper;
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
            var builder = services.ConfigureJpAdmin<AspNetUser>();
            switch (database)
            {
                case "MYSQL":
                    builder.WithMySql(connString);
                    break;
                case "SQLSERVER":
                    builder.WithSqlServer(connString);
                    break;
                case "POSTGRESQL":
                    builder.WithPostgreSql(connString);
                    break;
                case "SQLITE":
                    builder.WithSqlite(connString);
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
