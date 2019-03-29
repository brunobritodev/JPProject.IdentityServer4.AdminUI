using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class SaveClientCommandValidation : ClientValidation<SaveClientCommand>
    {
        public SaveClientCommandValidation()
        {
            ValidateClientId();
            ValidateClientName();

        }
    }
}