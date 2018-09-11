using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Data.Repository
{
    public class ClientRepository : Repository<Client> , IClientRepository
    {
        public ClientRepository(JpContext context) : base(context)
        {
        }
    }
}
