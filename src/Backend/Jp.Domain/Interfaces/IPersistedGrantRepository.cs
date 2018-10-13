using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IPersistedGrantRepository : IRepository<PersistedGrant>
    {
        Task<List<PersistedGrant>> GetGrants();
    }
}