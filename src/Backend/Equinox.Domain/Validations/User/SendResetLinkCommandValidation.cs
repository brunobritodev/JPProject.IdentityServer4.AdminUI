using Equinox.Domain.Commands.User;

namespace Equinox.Domain.Validations.User
{
    public class SendResetLinkCommandValidation : UserValidation<SendResetLinkCommand>
    {
        public SendResetLinkCommandValidation()
        {
            ValidateUsername();
            ValidateEmail();
        }
    }
}