using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Data.Repository
{
    public class ApiResourceRepository : Repository<ApiResource> , IApiResourceRepository
    {
        public ApiResourceRepository(JpContext context) : base(context)
        {
        }
    }
}