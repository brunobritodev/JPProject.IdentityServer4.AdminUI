using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jp.Application.ViewModels;
using Jp.Domain.Core.ViewModels;

namespace Jp.Application.Interfaces
{
    public interface IPersistedGrantAppService: IDisposable
    {
        Task<ListOfPersistedGrantViewModel> GetPersistedGrants(PagingViewModel paging);
        Task Remove(RemovePersistedGrantViewModel model);
    }
}