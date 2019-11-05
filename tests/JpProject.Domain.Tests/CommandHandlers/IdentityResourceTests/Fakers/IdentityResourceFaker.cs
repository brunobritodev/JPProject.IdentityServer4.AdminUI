using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using IdentityServer4.Models;

namespace JpProject.Domain.Tests.CommandHandlers.IdentityResourceTests.Fakers
{
    public class IdentityResourceFaker
    {
        public static Faker<IdentityResource> GenerateIdentiyResource()
        {
            return new Faker<IdentityResource>()
                .RuleFor(i => i.Required, f => f.Random.Bool())
                .RuleFor(i => i.Emphasize, f => f.Random.Bool())
                .RuleFor(i => i.ShowInDiscoveryDocument, f => f.Random.Bool())
                .RuleFor(i => i.Enabled, f => f.Random.Bool())
                .RuleFor(i => i.Name, f => f.Lorem.Word())
                .RuleFor(i => i.DisplayName, f => f.Lorem.Word())
                .RuleFor(i => i.Description, f => f.Lorem.Word());
        }
    }
}
