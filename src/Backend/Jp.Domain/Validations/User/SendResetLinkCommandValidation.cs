using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
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