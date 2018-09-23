using System;
using System.Collections.Generic;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.ApiResource
{
    public abstract class ApiResourceCommand : Command
    {

        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ApiSecret> Secrets { get; set; }
        public List<ApiScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }

    }
}