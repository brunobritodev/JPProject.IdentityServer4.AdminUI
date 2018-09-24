using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Domain.Interfaces
{
    public interface IApiResourceRepository : IRepository<ApiResource>
    {
        Task<List<string>> GetScopes(string search);
    }
}