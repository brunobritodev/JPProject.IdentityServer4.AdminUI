using System.Collections.Generic;
using System.Security.Claims;

namespace Equinox.Domain.Interfaces
{
    public interface ISystemUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
