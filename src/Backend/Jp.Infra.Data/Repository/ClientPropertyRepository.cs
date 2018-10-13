using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class ClientPropertyRepository : Repository<ClientProperty>, IClientPropertyRepository
    {
        public ClientPropertyRepository(JpContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClientProperty>> GetByClientId(string clientId)
        {
            return await DbSet.Where(w => w.Client.ClientId == clientId).ToListAsync();
        }
    }
}