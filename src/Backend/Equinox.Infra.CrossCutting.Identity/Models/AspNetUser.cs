using System.Collections.Generic;
using System.Security.Claims;
using Equinox.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Equinox.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : ISystemUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Username => _accessor.HttpContext.User.FindFirst("email")?.Value;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return  _accessor.HttpContext.User.Claims;
        }
    }
}
