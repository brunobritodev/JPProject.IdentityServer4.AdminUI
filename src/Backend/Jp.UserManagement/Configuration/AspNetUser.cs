using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Jp.Domain.Core.StringUtils;
using Jp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Jp.Management.Configuration
{
    public class AspNetUser : ISystemUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Username => GetUsername();

        private string GetUsername()
        {
            var username = _accessor.HttpContext.User.FindFirst("username")?.Value;
            if (username.IsPresent()) return username;

            var name = _accessor.HttpContext.User.Identity.Name;
            if (name.IsPresent()) return name;

            var sub = _accessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject);
            if (sub != null) return sub.Value;

            return string.Empty;
        }

        public Guid UserId => Guid.Parse(_accessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject)?.Value);
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public string GetRemoteIpAddress()
        {
            return _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        public string GetLocalIpAddress()
        {
            return _accessor.HttpContext.Connection.LocalIpAddress.ToString();
        }
    }
}