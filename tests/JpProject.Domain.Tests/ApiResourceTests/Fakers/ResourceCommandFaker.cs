using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using IdentityServer4.Models;
using Jp.Domain.Commands.ApiResource;

namespace JpProject.Domain.Tests.ApiResourceTests.Fakers
{
    public class ResourceCommandFaker
    {
        public static Faker<RegisterApiResourceCommand> GenerateRegisterApiResourceCommand()
        {
            return new Faker<RegisterApiResourceCommand>().CustomInstantiator(faker =>
                new RegisterApiResourceCommand(ApiResourceFaker.GenerateApiResource().Generate())
            );

        }
        public static Faker<UpdateApiResourceCommand> GenerateUpdateApiResourceCommand()
        {
            return new Faker<UpdateApiResourceCommand>().CustomInstantiator(faker =>
                new UpdateApiResourceCommand(ApiResourceFaker.GenerateApiResource().Generate())
            );

        }
        public static Faker<RemoveApiResourceCommand> GenerateRemoveApiResourceCommand()
        {
            return new Faker<RemoveApiResourceCommand>().CustomInstantiator(faker =>
                new RemoveApiResourceCommand(faker.Random.Word())
            );

        }
    }
}
