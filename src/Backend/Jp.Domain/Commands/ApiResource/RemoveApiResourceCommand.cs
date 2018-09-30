using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Commands.ApiResource
{
    public class RemoveApiResourceCommand : ApiResourceCommand
    {
        public RemoveApiResourceCommand(string name)
        {
            Resource = new IdentityServer4.Models.ApiResource() { Name = name };
        }


        public override bool IsValid()
        {
            ValidationResult = new RemoveApiResourceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}