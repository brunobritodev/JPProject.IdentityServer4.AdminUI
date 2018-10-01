using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jp.Application.ViewModels;

namespace Jp.Application.Interfaces
{
    public interface IPersistedGrantAppService: IDisposable
    {
        Task<IEnumerable<PersistedGrantViewModel>> GetPersistedGrants();
    }
}