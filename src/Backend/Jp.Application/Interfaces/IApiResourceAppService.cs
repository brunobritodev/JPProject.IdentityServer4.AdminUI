using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.ViewModels;
using Jp.Application.ViewModels.ApiResouceViewModels;
using Jp.Application.ViewModels.ClientsViewModels;
using Jp.Application.ViewModels.IdentityResourceViewModels;

namespace Jp.Application.Interfaces
{
    public interface IApiResourceAppService: IDisposable
    {
        Task<IEnumerable<ApiResourceListViewModel>> GetApiResources();
        Task<ApiResource> GetDetails(string name);
        Task Save(ApiResource model);
        Task Update(ApiResource model);
        Task Remove(RemoveApiResourceViewModel model);
        Task<IEnumerable<SecretViewModel>> GetSecrets(string name);
        Task RemoveSecret(RemoveApiSecretViewModel model);
        Task SaveSecret(SaveApiSecretViewModel model);
    }
}