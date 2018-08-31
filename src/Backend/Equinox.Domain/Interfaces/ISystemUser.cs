using System.Collections.Generic;
using System.Security.Claims;

namespace Equinox.Domain.Interfaces
{
    public interface ISystemUser
    {
        string Username { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
