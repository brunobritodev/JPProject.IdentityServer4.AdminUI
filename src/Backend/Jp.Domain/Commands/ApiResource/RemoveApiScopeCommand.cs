using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Commands.ApiResource
{
    public class RemoveApiScopeCommand : ApiScopeCommand
    {
        public RemoveApiScopeCommand(int id, string resourceName)
        {
            ResourceName = resourceName;
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveApiScopeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}