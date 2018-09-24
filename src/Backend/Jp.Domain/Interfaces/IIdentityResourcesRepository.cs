using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IIdentityResourcesRepository : IRepository<IdentityResource>
    {
        Task<List<string>> GetScopes(string search);
    }
}