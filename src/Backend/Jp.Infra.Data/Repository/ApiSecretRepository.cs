using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Repository
{
    public class ApiSecretRepository : Repository<ApiSecret>, IApiSecretRepository
    {
        public ApiSecretRepository(JpContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ApiSecret>> GetByApiName(string name)
        {
            return await DbSet.Where(s => s.ApiResource.Name == name).ToListAsync();
        }
    }
}