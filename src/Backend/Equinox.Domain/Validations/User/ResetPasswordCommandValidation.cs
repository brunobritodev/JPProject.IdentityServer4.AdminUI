using Equinox.Domain.Commands.User;
using FluentValidation;

namespace Equinox.Domain.Validations.User
{
    public class ResetPasswordCommandValidation : UserValidation<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidation()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateCode();
        }

        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty();
        }

    }
}