using Jp.Domain.Commands.Clients;

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