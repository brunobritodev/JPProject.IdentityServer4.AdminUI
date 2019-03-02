using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Jp.Infra.Data.Context
{
    public class JpContext : DbContext
        , IConfigurationDbContext
        , IPersistedGrantDbContext
    {
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalOptions;

        public JpContext(DbContextOptions<JpContext> options,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalOptions
            ) : base(options)
        {
            _storeOptions = storeOptions;
            _operationalOptions = operationalOptions;
        }


        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        public DbSet<IdentityClaim> IdentityClaims { get; set; }

        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        public DbSet<ClientClaim> ClientClaims { get; set; }

        public DbSet<ClientProperty> ClientProperties { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureClientContext(_storeOptions);
            modelBuilder.ConfigureResourcesContext(_storeOptions);
            modelBuilder.ConfigurePersistedGrantContext(_operationalOptions);
        }



        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }

}
