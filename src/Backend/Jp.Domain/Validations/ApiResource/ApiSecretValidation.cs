using FluentValidation;
using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.ApiResource
{
    public abstract class ApiSecretValidation<T> : AbstractValidator<T> where T : ApiSecretCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid secret");
        }
        protected void ValidateResourceName()
        {
            RuleFor(c => c.ResourceName).NotEmpty().WithMessage("ClientId must be set");
        }

        protected void ValidateValue()
        {
            RuleFor(c => c.Value).NotEmpty().WithMessage("Secret must be set");
        }
        protected void ValidateType()
        {
            RuleFor(c => c.Type).NotEmpty().WithMessage("Please ensure you have selected Type");
        }

        protected void ValidateHashType()
        {
            RuleFor(c => c.Hash).InclusiveBetween(0, 1).WithMessage("Please set hash type");
        }
    }
}