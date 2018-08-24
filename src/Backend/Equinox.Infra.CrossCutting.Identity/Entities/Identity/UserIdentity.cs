using System;
using Equinox.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Equinox.Infra.CrossCutting.Identity.Entities.Identity
{
    public class UserIdentity : IdentityUser<Guid>, IDomainUser
    {
        public string Picture { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Bio { get; set; }
        public string JobTitle { get; set; }
    }
}