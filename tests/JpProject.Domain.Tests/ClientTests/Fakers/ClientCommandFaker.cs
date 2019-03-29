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

        public static Faker<UpdateClientCommand> GenerateUpdateClientCommand()
        {
            return new Faker<UpdateClientCommand>().CustomInstantiator(f =>
                new UpdateClientCommand(
                    ClientFaker.GenerateClient().Generate(),
                    f.Company.CompanyName()
                ));

        }
    }
}
