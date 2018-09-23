using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetByUniqueName(string clientId);
    }
}
