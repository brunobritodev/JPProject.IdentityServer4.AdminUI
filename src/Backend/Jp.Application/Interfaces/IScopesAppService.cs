using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jp.Application.Interfaces
{
    public interface IScopesAppService : IDisposable
    {
        Task<IEnumerable<string>> GetScopes(string search);

    }
}