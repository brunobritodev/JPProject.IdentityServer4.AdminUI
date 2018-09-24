using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class IdentityResourcesRepository : Repository<IdentityResource> , IIdentityResourcesRepository
    {
        public IdentityResourcesRepository(JpContext context) : base(context)
        {
        }

        public Task<List<string>> GetScopes(string search) => DbSet.Where(id => id.Name.Contains(search)).Select(x => x.Name).ToListAsync();
    }
}