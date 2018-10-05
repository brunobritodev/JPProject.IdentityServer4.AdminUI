using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Jp.Domain.Interfaces
{
    public interface ISystemUser
    {
        string Username { get; }
        bool IsAuthenticated();
        Guid UserId { get; }
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
