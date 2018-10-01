using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IApiResourceRepository : IRepository<ApiResource>
    {
        Task<List<ApiResource>> GetResources();
        Task<ApiResource> GetByName(string name);
        Task UpdateWithChildrens(ApiResource irs);
        Task<ApiResource> GetResource(string resourceName);
    }
}