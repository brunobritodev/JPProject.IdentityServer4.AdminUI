using Equinox.Domain.Commands.User;
using FluentValidation;

namespace Equinox.Domain.Validations.User
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