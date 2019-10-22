using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using IdentityServer4.EntityFramework.Entities;

namespace JpProject.Domain.Tests.CommandHandlers.IdentityResourceTests.Fakers
{
    public class EntityIdentityResourceFaker
    {

        public static Faker<IdentityResource> GenerateEntity()
        {
            return new Faker<IdentityResource>()
                .RuleFor(i => i.Id, f => f.Random.Int())
                .RuleFor(i => i.Enabled, f => f.Random.Bool())
                .RuleFor(i => i.Name, f => f.Lorem.Word())
                .RuleFor(i => i.DisplayName, f => f.Lorem.Word())
                .RuleFor(i => i.Description, f => f.Lorem.Word())
                .RuleFor(i => i.Required, f => f.Random.Bool())
                .RuleFor(i => i.Emphasize, f => f.Random.Bool())
                .RuleFor(i => i.ShowInDiscoveryDocument, f => f.Random.Bool())
                .RuleFor(i => i.Created, f => f.Date.Past())
                .RuleFor(i => i.NonEditable, f => f.Random.Bool());
        }
    }
}
