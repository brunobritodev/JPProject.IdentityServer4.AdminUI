using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.IdentityResource
{
    public abstract class IdentityResourceCommand : Command
    {
        public IdentityServer4.Models.IdentityResource Resource { get; protected set; }
        
        public string OldIdentityResourceName { get; protected set; }

    }
}