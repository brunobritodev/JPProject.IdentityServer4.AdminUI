using System;
using Jp.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jp.Infra.CrossCutting.Identity.Entities.Identity
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