using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(JpContext context) : base(context)
        {
        }

        public Task<Client> GetClient(string clientId)
        {
            return DbSet
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.Properties)
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .SingleOrDefaultAsync();
        }

        public Task<Client> GetByClientId(string clientId)
        {
            return DbSet.Where(x => x.ClientId == clientId).SingleOrDefaultAsync();
        }

        public Task<Client> GetClientDefaultDetails(string clientId)
        {
            return DbSet
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateWithChildrens(Client client)
        {
            await RemoveClientRelationsAsync(client);
            Update(client);
        }


        private async Task RemoveClientRelationsAsync(Client client)
        {
            //Remove old allowed scopes
            var clientScopes = await Db.ClientScopes.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientScopes.RemoveRange(clientScopes);

            //Remove old grant types
            var clientGrantTypes = await Db.ClientGrantTypes.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientGrantTypes.RemoveRange(clientGrantTypes);

            //Remove old redirect uri
            var clientRedirectUris = await Db.ClientRedirectUris.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientRedirectUris.RemoveRange(clientRedirectUris);

            //Remove old client cors
            var clientCorsOrigins = await Db.ClientCorsOrigins.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientCorsOrigins.RemoveRange(clientCorsOrigins);

            //Remove old client id restrictions
            var clientIdPRestrictions = await Db.ClientIdPRestrictions.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientIdPRestrictions.RemoveRange(clientIdPRestrictions);

            //Remove old client post logout redirect
            var clientPostLogoutRedirectUris = await Db.ClientPostLogoutRedirectUris.Where(x => x.Client.Id == client.Id).ToListAsync();
            Db.ClientPostLogoutRedirectUris.RemoveRange(clientPostLogoutRedirectUris);
        }

    }
}
