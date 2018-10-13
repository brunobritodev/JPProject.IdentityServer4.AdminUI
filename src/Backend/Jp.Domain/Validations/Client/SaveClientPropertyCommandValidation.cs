using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class SaveClientPropertyCommandValidation : ClientPropertyValidation<SaveClientPropertyCommand>
    {
        public SaveClientPropertyCommandValidation()
        {
            ValidateClientId();
            ValidateKey();
            ValidateValue();
        }
    }
}