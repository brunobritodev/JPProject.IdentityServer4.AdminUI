using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Data.Repository
{
    public class ClientSecretRepository : Repository<ClientSecret>, IClientSecretRepository
    {
        public ClientSecretRepository(JpContext context) : base(context)
        {
        }
    }
}