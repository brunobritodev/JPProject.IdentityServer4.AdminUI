using FluentValidation;
using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
{
    public class ProfileValidation<T> : AbstractValidator<T> where T : ProfileCommand
    {

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Username")
                .Length(2, 150).WithMessage("The Username must have between 2 and 150 characters");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Invalid user");
        }

        protected void ValidatePicture()
        {
            RuleFor(c => c.Picture)
                .NotEmpty().WithMessage("Please ensure you have entered the picture");
        }
    }
}
