using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Core.ViewModels;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jp.Infra.Data.Repository
{
    public class PersistedGrantRepository : Repository<PersistedGrant>, IPersistedGrantRepository
    {
        public PersistedGrantRepository(JpContext context) : base(context)
        {
        }

        public Task<List<PersistedGrant>> GetGrants(PagingViewModel paging)
        {
            return DbSet.AsNoTracking().OrderByDescending(s => s.CreationTime).Skip(paging.Offset).Take(paging.Limit).ToListAsync();
        }

        public Task<int> Count()
        {
            return DbSet.CountAsync();
        }
    }
}