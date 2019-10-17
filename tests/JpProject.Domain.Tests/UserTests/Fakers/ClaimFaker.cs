using Bogus;
using System.Security.Claims;

namespace JpProject.Domain.Tests.UserTests.Fakers
{
    public static class ClaimFaker
    {
        public static Faker<Claim> GenerateClaim(string type = null, string value = null)
        {
            return new Faker<Claim>()
                .RuleFor(c => c.Issuer, f => f.Lorem.Word())
                .RuleFor(c => c.OriginalIssuer, f => f.Lorem.Word())
                .RuleFor(c => c.Properties, f => default)
                .RuleFor(c => c.Subject, f => default)
                .RuleFor(c => c.Type, f => type ?? f.Lorem.Word())
                .RuleFor(c => c.Value, f => value ?? f.Lorem.Word())
                .RuleFor(c => c.ValueType, f => f.Lorem.Word());
        }
    }
}
