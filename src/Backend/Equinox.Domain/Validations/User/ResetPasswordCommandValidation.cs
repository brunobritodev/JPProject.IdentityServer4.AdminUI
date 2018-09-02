using FluentValidation;
using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
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