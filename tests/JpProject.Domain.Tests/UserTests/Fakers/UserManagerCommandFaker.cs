using Bogus;
using Jp.Domain.Commands.User;

namespace JpProject.Domain.Tests.UserTests.Fakers
{
    public static class UserManagerCommandFaker
    {
        public static Faker<RemoveUserClaimCommand> GenerateRemoveClaimCommand(bool value = true)
        {
            return new Faker<RemoveUserClaimCommand>().CustomInstantiator(
                f => new RemoveUserClaimCommand(
                    f.Person.UserName,
                    f.Company.CompanyName(),
                    value ? f.Commerce.Department() : null
                )
            );
        }
    }
}
