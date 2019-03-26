using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using System.Threading.Tasks;
using Jp.Domain.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class PersistedGrantRepository : Repository<PersistedGrant>, IPersistedGrantRepository
    {
        public PersistedGrantRepository(JpContext context) : base(context)
        {
        }

        public Task<List<PersistedGrant>> GetGrants(PagingViewModel paging)
        {
            return DbSet.AsNoTracking().OrderByDescending(s => s.CreationTime).Skip((paging.Page - 1) * paging.Quantity).Take(paging.Quantity).ToListAsync();
        }

        public Task<int> Count()
        {
            return DbSet.CountAsync();
        }
    }
}