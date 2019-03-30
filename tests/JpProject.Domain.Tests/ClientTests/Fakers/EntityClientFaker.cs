using Bogus;
using IdentityServer4.EntityFramework.Entities;

namespace JpProject.Domain.Tests.ClientTests.Fakers
{
    public class EntityClientFaker
    {
        public static Faker<ClientProperty> GenerateClientProperty()
        {
            return new Faker<ClientProperty>()
                .RuleFor(c => c.ClientId, f => f.Random.Int(0))
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Key, f => f.Lorem.Word())
                .RuleFor(c => c.Value, f => f.Lorem.Word());


        }
        public static Faker<ClientCorsOrigin> GenerateClientCorsOrigin()
        {
            return new Faker<ClientCorsOrigin>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Origin, f => f.Internet.Url())
                .RuleFor(c => c.ClientId, f => f.Random.Int());


        }
        public static Faker<ClientClaim> GenerateClientClaim()
        {
            return new Faker<ClientClaim>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Type, f => f.PickRandom(IdentityHelpers.Claims))
                .RuleFor(c => c.Value, f => f.Lorem.Word())
                .RuleFor(c => c.ClientId, f => f.Random.Int());


        }
        public static Faker<ClientIdPRestriction> GenerateClientIdPRestriction()
        {
            return new Faker<ClientIdPRestriction>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Provider, f => f.PickRandom(IdentityHelpers.Providers))
                .RuleFor(c => c.ClientId, f => f.Random.Int(0));

        }
        public static Faker<ClientScope> GenerateClientScope()
        {
            return new Faker<ClientScope>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Scope, f => f.PickRandom(IdentityHelpers.Scopes))
                .RuleFor(c => c.ClientId, f => f.Random.Int(0));
        }
        public static Faker<ClientPostLogoutRedirectUri> GenerateClientPostLogoutRedirectUri()
        {
            return new Faker<ClientPostLogoutRedirectUri>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.PostLogoutRedirectUri, f => f.Internet.Url())
                .RuleFor(c => c.ClientId, f => f.Random.Int(0));
        }

        public static Faker<ClientRedirectUri> GenerateClientRedirectUri()
        {
            return new Faker<ClientRedirectUri>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.RedirectUri, f => f.Internet.Url())
                .RuleFor(c => c.ClientId, f => f.Random.Int(0));
        }

        public static Faker<ClientGrantType> GenerateClientGrantType()
        {
            return new Faker<ClientGrantType>()
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.GrantType, f => f.PickRandom(IdentityHelpers.Grantypes))
                .RuleFor(c => c.ClientId, f => f.Random.Int(0));
        }

        public static Faker<ClientSecret> GenerateClientSecret()
        {
            return new Faker<ClientSecret>()
                .RuleFor(c => c.ClientId, f => f.Random.Int(0))
                .RuleFor(c => c.Id, f => f.Random.Int(0))
                .RuleFor(c => c.Description, f => f.Lorem.Word())
                .RuleFor(c => c.Value, f => f.Lorem.Word())
                .RuleFor(c => c.Type, f => f.Lorem.Word())
                .RuleFor(c => c.Created, f => f.Date.Past());
        }
        public static Faker<Client> GenerateClient(
            int? clientClaim = null,
            string clientId = null, 
            int? clientSecrets = null,
            int? clientProperties = null)
        {
            return new Faker<Client>()
                .RuleFor(c => c.Id, f =>  f.Random.Int(0))
                .RuleFor(c => c.Enabled, f => f.Random.Bool())
                .RuleFor(c => c.ClientId, f => clientId ?? f.Lorem.Word())
                .RuleFor(c => c.ProtocolType, f => f.Lorem.Word())
                .RuleFor(c => c.RequireClientSecret, f => f.Random.Bool())
                .RuleFor(c => c.ClientName, f => f.Lorem.Word())
                .RuleFor(c => c.Description, f => f.Lorem.Word())
                .RuleFor(c => c.ClientUri, f => f.Lorem.Word())
                .RuleFor(c => c.LogoUri, f => f.Lorem.Word())
                .RuleFor(c => c.RequireConsent, f => f.Random.Bool())
                .RuleFor(c => c.AllowRememberConsent, f => f.Random.Bool())
                .RuleFor(c => c.AlwaysIncludeUserClaimsInIdToken, f => f.Random.Bool())
                .RuleFor(c => c.RequirePkce, f => f.Random.Bool())
                .RuleFor(c => c.AllowPlainTextPkce, f => f.Random.Bool())
                .RuleFor(c => c.AllowAccessTokensViaBrowser, f => f.Random.Bool())
                .RuleFor(c => c.FrontChannelLogoutUri, f => f.Lorem.Word())
                .RuleFor(c => c.FrontChannelLogoutSessionRequired, f => f.Random.Bool())
                .RuleFor(c => c.BackChannelLogoutUri, f => f.Lorem.Word())
                .RuleFor(c => c.BackChannelLogoutSessionRequired, f => f.Random.Bool())
                .RuleFor(c => c.AllowOfflineAccess, f => f.Random.Bool())
                .RuleFor(c => c.IdentityTokenLifetime, f => f.Random.Int())
                .RuleFor(c => c.AccessTokenLifetime, f => f.Random.Int())
                .RuleFor(c => c.AuthorizationCodeLifetime, f => f.Random.Int())
                .RuleFor(c => c.AbsoluteRefreshTokenLifetime, f => f.Random.Int())
                .RuleFor(c => c.SlidingRefreshTokenLifetime, f => f.Random.Int())
                .RuleFor(c => c.RefreshTokenUsage, f => f.Random.Int())
                .RuleFor(c => c.UpdateAccessTokenClaimsOnRefresh, f => f.Random.Bool())
                .RuleFor(c => c.RefreshTokenExpiration, f => f.Random.Int())
                .RuleFor(c => c.AccessTokenType, f => f.Random.Int())
                .RuleFor(c => c.EnableLocalLogin, f => f.Random.Bool())
                .RuleFor(c => c.IncludeJwtId, f => f.Random.Bool())
                .RuleFor(c => c.AlwaysSendClientClaims, f => f.Random.Bool())
                .RuleFor(c => c.ClientClaimsPrefix, f => f.Lorem.Word())
                .RuleFor(c => c.PairWiseSubjectSalt, f => f.Lorem.Word())
                .RuleFor(c => c.Created, f => f.Date.Past())
                .RuleFor(c => c.UserCodeType, f => f.Lorem.Word())
                .RuleFor(c => c.DeviceCodeLifetime, f => f.Random.Int())
                .RuleFor(c => c.NonEditable, f => f.Random.Bool())
                .RuleFor(c => c.AllowedScopes, f => GenerateClientScope().Generate(f.Random.Int(1, 4)))
                .RuleFor(c => c.ClientSecrets, f => GenerateClientSecret().Generate(clientSecrets ?? f.Random.Int(0, 3)))
                .RuleFor(c => c.AllowedGrantTypes, f => GenerateClientGrantType().Generate(1))
                .RuleFor(c => c.RedirectUris, f => GenerateClientRedirectUri().Generate(f.Random.Int(1, 3)))
                .RuleFor(c => c.PostLogoutRedirectUris, f => GenerateClientPostLogoutRedirectUri().Generate(f.Random.Int(0, 2)))
                .RuleFor(c => c.Claims, f => GenerateClientClaim().Generate(clientClaim ?? f.Random.Int(1, 5)))
                .RuleFor(c => c.IdentityProviderRestrictions, f => GenerateClientIdPRestriction().Generate(f.Random.Int(0, 1)))
                .RuleFor(c => c.AllowedCorsOrigins, f => GenerateClientCorsOrigin().Generate(f.Random.Int(0, 2)))
                .RuleFor(c => c.Properties, f => GenerateClientProperty().Generate(clientProperties ?? f.Random.Int(0, 2)));

        }
    }
}
