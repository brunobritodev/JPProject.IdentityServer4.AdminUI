using FluentValidation;
using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public abstract class ClientPropertyValidation<T> : AbstractValidator<T> where T : ClientPropertyCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid secret");
        }
        protected void ValidateClientId()
        {
            RuleFor(c => c.ClientId).NotEmpty().WithMessage("ClientId must be set");
        }

        protected void ValidateValue()
        {
            RuleFor(c => c.Value).NotEmpty().WithMessage("Secret must be set");
        }
        protected void ValidateKey()
        {
            RuleFor(c => c.Key).NotEmpty().WithMessage("Please ensure you have entered key");
        }
    }
}