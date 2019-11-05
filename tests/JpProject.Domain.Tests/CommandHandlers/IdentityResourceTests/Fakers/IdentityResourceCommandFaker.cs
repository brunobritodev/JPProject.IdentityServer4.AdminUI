using Bogus;
using IdentityServer4.Models;
using Jp.Domain.Commands.IdentityResource;

namespace JpProject.Domain.Tests.CommandHandlers.IdentityResourceTests.Fakers
{
    public class IdentityResourceCommandFaker
    {
        public static Faker<RegisterIdentityResourceCommand> GenerateRegisterCommand(IdentityResource id = null)
        {
            return new Faker<RegisterIdentityResourceCommand>().CustomInstantiator(faker =>
                new RegisterIdentityResourceCommand(IdentityResourceFaker.GenerateIdentiyResource().Generate()));
        }

        public static Faker<UpdateIdentityResourceCommand> GenerateUpdateCommand(IdentityResource id = null, string oldIdentityResourceName = null)
        {
            return new Faker<UpdateIdentityResourceCommand>().CustomInstantiator(faker =>
                new UpdateIdentityResourceCommand(IdentityResourceFaker.GenerateIdentiyResource().Generate(), oldIdentityResourceName ?? faker.Internet.DomainName()));
        }


        public static Faker<RemoveIdentityResourceCommand> GenerateRemoveCommand(string oldIdentityResourceName = null)
        {
            return new Faker<RemoveIdentityResourceCommand>().CustomInstantiator(faker =>
                new RemoveIdentityResourceCommand(oldIdentityResourceName ?? faker.Internet.DomainName()));
        }
    }
}
