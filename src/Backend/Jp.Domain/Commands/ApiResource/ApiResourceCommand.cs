using System;
using System.Collections.Generic;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.ApiResource
{
    public abstract class ApiResourceCommand : Command
    {
        public IdentityServer4.Models.ApiResource Resource { get; protected set; }


    }
}