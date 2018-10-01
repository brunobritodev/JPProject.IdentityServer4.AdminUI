using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IIdentityResourceRepository : IRepository<IdentityResource>
    {
        Task<IdentityResource> GetByName(string name);
        Task UpdateWithChildrens(IdentityResource irs);
        Task<IdentityResource> GetDetails(string name);
        Task<List<string>> SearchScopes(string search);
    }
}