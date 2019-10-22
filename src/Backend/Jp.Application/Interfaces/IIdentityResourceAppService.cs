using IdentityServer4.Models;
using Jp.Application.ViewModels.IdentityResourceViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Application.Interfaces
{
    public interface IIdentityResourceAppService : IDisposable
    {
        Task<IEnumerable<IdentityResourceListView>> GetIdentityResources();
        Task<IdentityResource> GetDetails(string name);
        Task Save(IdentityResource model);
        Task Update(string resource, IdentityResource model);
        Task Remove(RemoveIdentityResourceViewModel model);
    }
}