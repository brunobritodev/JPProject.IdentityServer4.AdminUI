using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClient(string clientId);
        Task UpdateWithChildrens(Client client);
        Task<Client> GetByClientId(string requestClientId);

        Task<Client> GetClientDefaultDetails(string clientId);
    }
}
