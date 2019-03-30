using Bogus;
using IdentityServer4.EntityFramework.Entities;

namespace JpProject.Domain.Tests.ApiResourceTests.Fakers
{
    public class EntityResourceFaker
    {
        public static Faker<ApiResource> GenerateResource()
        {
            return new Faker<ApiResource>()
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Enabled, f => f.Random.Bool())
                .RuleFor(a => a.Name, f => f.Lorem.Word())
                .RuleFor(a => a.DisplayName, f => f.Lorem.Word())
                .RuleFor(a => a.Description, f => f.Lorem.Word())
                .RuleFor(a => a.Secrets, f => GenerateSecrets().Generate(f.Random.Int(0, 3)))
                .RuleFor(a => a.Scopes, f => GenerateScopes().Generate(f.Random.Int(0, 3)))
                .RuleFor(a => a.UserClaims, f => GenerateUserClaims().Generate(f.Random.Int(0, 2)))
                .RuleFor(a => a.Properties, f => GenerateProperties().Generate(f.Random.Int(0, 3)))
                .RuleFor(a => a.NonEditable, f => f.Random.Bool());
        }

        public static Faker<ApiSecret> GenerateSecrets()
        {
            return new Faker<ApiSecret>()
                .RuleFor(a => a.ApiResourceId, f => f.Random.Int(0))
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Description, f => f.Lorem.Word())
                .RuleFor(a => a.Value, f => f.Lorem.Word())
                .RuleFor(a => a.Type, f => f.Lorem.Word())
                .RuleFor(a => a.Created, f => f.Date.Past());
        }
        public static Faker<ApiScope> GenerateScopes()
        {
            return new Faker<ApiScope>()
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Name, f => f.Lorem.Word())
                .RuleFor(a => a.DisplayName, f => f.Lorem.Word())
                .RuleFor(a => a.Description, f => f.Lorem.Word())
                .RuleFor(a => a.Required, f => f.Random.Bool())
                .RuleFor(a => a.Emphasize, f => f.Random.Bool())
                .RuleFor(a => a.ShowInDiscoveryDocument, f => f.Random.Bool())
                .RuleFor(a => a.UserClaims, f => GenerateApiScopeClaim().Generate(f.Random.Int(0, 2)))
                .RuleFor(a => a.ApiResourceId, f => f.Random.Int(0));
        }
        public static Faker<ApiScopeClaim> GenerateApiScopeClaim()
        {
            return new Faker<ApiScopeClaim>()
                .RuleFor(a => a.ApiScopeId, f => f.Random.Int(0))
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Type, f => f.Lorem.Word());
        }
        public static Faker<ApiResourceClaim> GenerateUserClaims()
        {
            return new Faker<ApiResourceClaim>()
                .RuleFor(a => a.ApiResourceId, f => f.Random.Int(0))
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Type, f => f.Lorem.Word());
        }
        public static Faker<ApiResourceProperty> GenerateProperties()
        {
            return new Faker<ApiResourceProperty>()
                .RuleFor(a => a.ApiResourceId, f => f.Random.Int(0))
                .RuleFor(a => a.Id, f => f.Random.Int(0))
                .RuleFor(a => a.Key, f => f.Lorem.Word())
                .RuleFor(a => a.Value, f => f.Lorem.Word());
        }
    }
}
