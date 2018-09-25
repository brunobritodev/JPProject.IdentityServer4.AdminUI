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
        Task<IEnumerable<Secret>> GetSecrets(string clientId);
    }
}