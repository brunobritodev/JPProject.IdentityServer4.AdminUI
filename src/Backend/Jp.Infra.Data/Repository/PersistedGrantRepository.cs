using System.Collections.Generic;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class PersistedGrantRepository : Repository<PersistedGrant>, IPersistedGrantRepository
    {
        public PersistedGrantRepository(JpContext context) : base(context)
        {
        }

        public Task<List<PersistedGrant>> GetGrants()
        {
            return DbSet.AsNoTracking().ToListAsync();
        }
    }
}