using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IClientClaimRepository : IRepository<ClientClaim>
    {
        Task<IEnumerable<ClientClaim>> GetByClientId(string clientId);
    }
}