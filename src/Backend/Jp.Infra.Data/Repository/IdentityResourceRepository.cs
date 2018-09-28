using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Data.Repository
{
    public class IdentityResourceRepository : Repository<IdentityResource> , IIdentityResourceRepository
    {
        public IdentityResourceRepository(JpContext context) : base(context)
        {
        }
    }
}