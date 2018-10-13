using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;

namespace Jp.Application.Interfaces
{
    public interface IIdentityResourceAppService: IDisposable
    {
        Task<IEnumerable<IdentityResourceListView>> GetIdentityResources();
        Task<IdentityResource> GetDetails(string name);
        Task Save(IdentityResource model);
        Task Update(IdentityResource model);
        Task Remove(RemoveIdentityResourceViewModel model);
    }
}