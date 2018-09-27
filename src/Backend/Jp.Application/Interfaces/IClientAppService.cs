using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Jp.Application.ViewModels.ClientsViewModels;

namespace Jp.Application.Interfaces
{
    public interface IClientAppService: IDisposable
    {
        Task<IEnumerable<ClientListViewModel>> GetClients();
        Task<Client> GetClientDetails(string clientId);
        Task Update(Client client);
        Task<IEnumerable<SecretViewModel>> GetSecrets(string clientId);
        Task RemoveSecret(RemoveSecretViewModel model);
        Task SaveSecret(SaveClientSecretViewModel model);
        Task<IEnumerable<ClientPropertyViewModel>> GetProperties(string clientId);
        Task RemoveProperty(RemovePropertyViewModel model);
        Task SaveProperty(SaveClientPropertyViewModel model);
        Task<IEnumerable<ClientClaimViewModel>> GetClaims(string clientId);
        Task RemoveClaim(RemoveClientClaimViewModel model);
        Task SaveClaim(SaveClientClaimViewModel model);
    }
}