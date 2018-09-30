using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class IdentityResourceRepository : Repository<IdentityResource> , IIdentityResourceRepository
    {
        public IdentityResourceRepository(JpContext context) : base(context)
        {
        }

        public Task<IdentityResource> GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(w => w.Name == name);
        }

        public async Task UpdateWithChildrens(IdentityResource irs)
        {
            await RemoveIdentityResourceClaimsAsync(irs);
            Update(irs);
        }

        public Task<IdentityResource> GetDetails(string name)
        {
            return DbSet.Include(s => s.UserClaims).AsNoTracking().FirstOrDefaultAsync(w => w.Name == name);
        }

        private async Task RemoveIdentityResourceClaimsAsync(IdentityResource identityResource)
        {
            var identityClaims = await Db.IdentityClaims.Where(x => x.IdentityResource.Id == identityResource.Id).ToListAsync();
            Db.IdentityClaims.RemoveRange(identityClaims);
        }
    }
}