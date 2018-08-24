using System;
using System.IO;
using System.Threading.Tasks;
using Equinox.Infra.CrossCutting.Identity.Constants;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Equinox.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, Guid, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("SSOConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureIdentityContext(builder);

        }


        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }


        private void ConfigureIdentityContext(ModelBuilder builder)
        {
            builder.Entity<UserIdentityRole>().ToTable(TableConsts.IdentityRoles);
            builder.Entity<UserIdentityRoleClaim>().ToTable(TableConsts.IdentityRoleClaims);
            builder.Entity<UserIdentityUserRole>().ToTable(TableConsts.IdentityUserRoles);

            builder.Entity<UserIdentity>().ToTable(TableConsts.IdentityUsers);
            builder.Entity<UserIdentityUserLogin>().ToTable(TableConsts.IdentityUserLogins);
            builder.Entity<UserIdentityUserClaim>().ToTable(TableConsts.IdentityUserClaims);
            builder.Entity<UserIdentityUserToken>().ToTable(TableConsts.IdentityUserTokens);
        }
    }
}
