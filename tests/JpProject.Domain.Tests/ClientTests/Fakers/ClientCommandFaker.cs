using Bogus;
using Jp.Domain.Commands.Client;

namespace JpProject.Domain.Tests.ClientTests.Fakers
{
    public class ClientCommandFaker
    {

        public static Faker<SaveClientCommand> GenerateSaveClientCommand()
        {
            return new Faker<SaveClientCommand>().CustomInstantiator(f =>
                new SaveClientCommand(
                    f.Lorem.Word(),
                    f.Company.CompanyName(),
                    f.Internet.Url(),
                    f.Image.LoremFlickrUrl(),
                    f.Lorem.Sentence(),
                    f.PickRandom<ClientType>()
                ));

        }
        public static Faker<CopyClientCommand> GenerateCopyClientCommand()
        {
            return new Faker<CopyClientCommand>().CustomInstantiator(f =>
                new CopyClientCommand(f.Lorem.Word()));

        }

        public static Faker<UpdateClientCommand> GenerateUpdateClientCommand(
            int? absoluteRefreshTokenLifetime = null,
            int? identityTokenLifetime = null,
            int? accessTokenLifetime = null,
            int? authorizationCodeLifetime = null,
            int? slidingRefreshTokenLifetime = null,
            int? deviceCodeLifetime = null,
            string oldClientId = null)
        {
            return new Faker<UpdateClientCommand>().CustomInstantiator(f =>
                new UpdateClientCommand(
                    ClientFaker.GenerateClient(absoluteRefreshTokenLifetime,
                                               identityTokenLifetime,
                                               accessTokenLifetime,
                                                authorizationCodeLifetime,
                        slidingRefreshTokenLifetime,
                        deviceCodeLifetime).Generate(),
                    oldClientId ?? f.Internet.DomainName()));

        }

        public static Faker<RemoveClientCommand> GenerateRemoveClientCommand()
        {
            return new Faker<RemoveClientCommand>().CustomInstantiator(f => new RemoveClientCommand(f.Lorem.Word()));
        }

        public static Faker<RemoveClientSecretCommand> GenerateRemoveClientSecretCommand(int? id = null)
        {
            return new Faker<RemoveClientSecretCommand>().CustomInstantiator(f => new RemoveClientSecretCommand(id ?? f.Random.Int(0), f.Lorem.Word()));
        }

        public static Faker<SaveClientSecretCommand> GenerateSaveClientSecretCommand()
        {
            return new Faker<SaveClientSecretCommand>().CustomInstantiator(f => new SaveClientSecretCommand(
                f.Lorem.Word(),
                f.Lorem.Sentence(),
                f.Lorem.Word(),
                f.Lorem.Word(),
                f.Date.Future(),
                f.Random.Int(0, 1)
                ));
        }

        public static Faker<RemovePropertyCommand> GenerateRemovePropertyCommand(int? id = null)
        {
            return new Faker<RemovePropertyCommand>().CustomInstantiator(f => new RemovePropertyCommand(
                id ?? f.Random.Int(0),
                f.Random.Word()
            ));
        }

        public static Faker<SaveClientPropertyCommand> GenerateSavePropertyCommand()
        {
            return new Faker<SaveClientPropertyCommand>().CustomInstantiator(f => new SaveClientPropertyCommand(
                f.Random.Word(),
                f.Random.Word(),
                f.Random.Word()
            ));
        }

        public static Faker<RemoveClientClaimCommand> GenerateRemoveClaimCommand(int? id = null)
        {
            return new Faker<RemoveClientClaimCommand>().CustomInstantiator(f => new RemoveClientClaimCommand(
                id ?? f.Random.Int(0),
                f.Random.Word()
            ));
        }
        public static Faker<SaveClientClaimCommand> GenerateSaveClaimCommand()
        {
            return new Faker<SaveClientClaimCommand>().CustomInstantiator(f => new SaveClientClaimCommand(
                f.Random.Word(),
                f.Random.Word(),
                f.Random.Word()
            ));
        }

    }
}
