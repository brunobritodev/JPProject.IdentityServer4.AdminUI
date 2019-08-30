using Jp.Domain.Commands.Clients;

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