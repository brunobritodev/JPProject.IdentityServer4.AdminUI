using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IClientSecretRepository : IRepository<ClientSecret>
    {
        Task<IEnumerable<ClientSecret>> GetByClientId(string clientId);
    }
}