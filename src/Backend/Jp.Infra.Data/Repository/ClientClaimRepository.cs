using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class ClientClaimRepository : Repository<ClientClaim>, IClientClaimRepository
    {
        public ClientClaimRepository(JpContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClientClaim>> GetByClientId(string clientId)
        {
            return await DbSet.Where(s => s.Client.ClientId == clientId).ToListAsync();
        }
    }
}