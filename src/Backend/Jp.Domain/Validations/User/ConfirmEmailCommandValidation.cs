using FluentValidation;
using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class ConfirmEmailCommandValidation : UserValidation<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidation()
        {
            ValidateEmail();
            ValidateCode();
        }

        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty();
        }

    }
}