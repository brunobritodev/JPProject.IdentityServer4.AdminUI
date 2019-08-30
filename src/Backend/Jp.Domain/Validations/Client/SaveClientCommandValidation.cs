using Jp.Domain.Commands.Clients;

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