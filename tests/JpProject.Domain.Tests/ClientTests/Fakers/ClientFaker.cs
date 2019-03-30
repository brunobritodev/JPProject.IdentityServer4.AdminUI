using Bogus;
using IdentityServer4.Models;
using System.Linq;
using System.Security.Claims;

namespace JpProject.Domain.Tests.ClientTests.Fakers
{
    public static class ClientFaker
    {
        public static Faker<Client> GenerateClient(
            int? absoluteRefreshTokenLifetime = null,
            int? identityTokenLifetime = null,
            int? accessTokenLifetime = null,
            int? authorizationCodeLifetime = null,
            int? slidingRefreshTokenLifetime = null,
            int? deviceCodeLifetime = null)
        {
            return new Faker<Client>()
                .RuleFor(c => c.Enabled, f => f.Random.Bool())
                .RuleFor(c => c.ClientId, f => f.Lorem.Word())
                .RuleFor(c => c.ProtocolType, f => f.Lorem.Word())
                .RuleFor(c => c.ClientSecrets, f => GenerateClientSecret().Generate(1))
                .RuleFor(c => c.RequireClientSecret, f => f.Random.Bool())
                .RuleFor(c => c.ClientName, f => f.Lorem.Word())
                .RuleFor(c => c.Description, f => f.Lorem.Sentences())
                .RuleFor(c => c.ClientUri, f => f.Lorem.Word())
                .RuleFor(c => c.LogoUri, f => f.Lorem.Word())
                .RuleFor(c => c.RequireConsent, f => f.Random.Bool())
                .RuleFor(c => c.AllowRememberConsent, f => f.Random.Bool())
                .RuleFor(c => c.AllowedGrantTypes, f => f.PickRandom(IdentityHelpers.Grantypes, 1).ToList())
                .RuleFor(c => c.RequirePkce, f => f.Random.Bool())
                .RuleFor(c => c.AllowPlainTextPkce, f => f.Random.Bool())
                .RuleFor(c => c.AllowAccessTokensViaBrowser, f => f.Random.Bool())
                .RuleFor(c => c.RedirectUris, f => Enumerable.Range(1, f.Random.Int(1, 3)).Select(x => f.PickRandom(f.Internet.Url())).ToList())
                .RuleFor(c => c.PostLogoutRedirectUris, f => Enumerable.Range(1, f.Random.Int(1, 3)).Select(x => f.PickRandom(f.Internet.Url())).ToList())
                .RuleFor(c => c.FrontChannelLogoutUri, f => f.Lorem.Word())
                .RuleFor(c => c.FrontChannelLogoutSessionRequired, f => f.Random.Bool())
                .RuleFor(c => c.BackChannelLogoutUri, f => f.Lorem.Word())
                .RuleFor(c => c.BackChannelLogoutSessionRequired, f => f.Random.Bool())
                .RuleFor(c => c.AllowOfflineAccess, f => f.Random.Bool())
                .RuleFor(c => c.AllowedScopes, f => f.PickRandom(IdentityHelpers.Scopes, f.Random.Int(1, 5)).ToList())
                .RuleFor(c => c.AlwaysIncludeUserClaimsInIdToken, f => f.Random.Bool())
                .RuleFor(c => c.UpdateAccessTokenClaimsOnRefresh, f => f.Random.Bool())
                .RuleFor(c => c.AccessTokenType, f => f.PickRandom<AccessTokenType>())
                .RuleFor(c => c.EnableLocalLogin, f => f.Random.Bool())
                .RuleFor(c => c.IdentityProviderRestrictions, f => f.PickRandom(IdentityHelpers.Providers, 1).ToList())
                .RuleFor(c => c.IncludeJwtId, f => f.Random.Bool())
                .RuleFor(c => c.Claims, f => GenerateClientClaim().Generate(f.Random.Int(1, 5)))
                .RuleFor(c => c.AlwaysSendClientClaims, f => f.Random.Bool())
                .RuleFor(c => c.ClientClaimsPrefix, f => f.Lorem.Word())
                .RuleFor(c => c.PairWiseSubjectSalt, f => f.Lorem.Word())
                .RuleFor(c => c.UserCodeType, f => f.Lorem.Word())
                .RuleFor(c => c.AllowedCorsOrigins, f => Enumerable.Range(1, f.Random.Int(1, 3)).Select(x => f.PickRandom(f.Internet.Url())).ToList())
                .RuleFor(c => c.IdentityTokenLifetime, f => identityTokenLifetime ?? f.Random.Int(0))
                .RuleFor(c => c.AccessTokenLifetime, f => accessTokenLifetime ?? f.Random.Int(0))
                .RuleFor(c => c.AuthorizationCodeLifetime, f => authorizationCodeLifetime ?? f.Random.Int(0))
                .RuleFor(c => c.AbsoluteRefreshTokenLifetime, f => absoluteRefreshTokenLifetime ?? f.Random.Int(0))
                .RuleFor(c => c.SlidingRefreshTokenLifetime, f => slidingRefreshTokenLifetime?? f.Random.Int(0))
                .RuleFor(c => c.DeviceCodeLifetime, f => deviceCodeLifetime ?? f.Random.Int(0));
        }

        public static Faker<Secret> GenerateClientSecret()
        {
            return new Faker<Secret>()
                .RuleFor(s => s.Description, f => f.Lorem.Word())
                .RuleFor(s => s.Value, f => f.Lorem.Word())
                .RuleFor(s => s.Type, f => f.PickRandom(IdentityHelpers.SecretTypes));
        }

        public static Faker<Claim> GenerateClientClaim()
        {
            return new Faker<Claim>().CustomInstantiator(f =>
                    new Claim(
                        f.PickRandom(IdentityHelpers.Claims),
                        f.Lorem.Word()
                    ));
        }
    }
}
