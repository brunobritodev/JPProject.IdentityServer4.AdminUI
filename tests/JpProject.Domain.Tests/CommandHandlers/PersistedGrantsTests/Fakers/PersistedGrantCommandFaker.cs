using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Jp.Domain.Commands.PersistedGrant;

namespace JpProject.Domain.Tests.CommandHandlers.PersistedGrantsTests.Fakers
{
    public class PersistedGrantCommandFaker
    {
        public static Faker<RemovePersistedGrantCommand> GenerateRemoveCommand(string name = null)
        {
            return new Faker<RemovePersistedGrantCommand>().CustomInstantiator(c => new RemovePersistedGrantCommand(c.Company.CompanyName()));
        }
    }
}
