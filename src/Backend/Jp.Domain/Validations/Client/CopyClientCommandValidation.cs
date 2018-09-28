using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class CopyClientCommandValidation : ClientValidation<CopyClientCommand>
    {
        public CopyClientCommandValidation()
        {
            ValidateClientId();
        }
    }
}