using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IClientSecretRepository : IRepository<ClientSecret>
    {
    }
}