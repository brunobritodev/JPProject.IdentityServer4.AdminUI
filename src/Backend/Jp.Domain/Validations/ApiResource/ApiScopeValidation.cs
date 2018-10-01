using FluentValidation;
using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.ApiResource
{
    public abstract class ApiScopeValidation<T> : AbstractValidator<T> where T : ApiScopeCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid scope");
        }
        protected void ValidateResourceName()
        {
            RuleFor(c => c.ResourceName).NotEmpty().WithMessage("ClientId must be set");
        }


    }
}