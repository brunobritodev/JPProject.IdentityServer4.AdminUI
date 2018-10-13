using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Commands.ApiResource
{
    public class RemoveApiSecretCommand : ApiSecretCommand
    {
        public RemoveApiSecretCommand(int id, string resourceName)
        {
            ResourceName = resourceName;
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveApiSecretCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}