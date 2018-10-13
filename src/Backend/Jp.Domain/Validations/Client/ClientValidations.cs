using System;
using FluentValidation;
using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public abstract class ClientValidation<T> : AbstractValidator<T> where T : ClientCommand
    {
        protected void ValidateGrantType()
        {
            RuleFor(c => c.Client.AllowedGrantTypes)
                .NotEmpty();
        }
        protected void ValidateClientId()
        {
            RuleFor(c => c.Client.ClientId).NotEmpty().WithMessage("ClientId must be set");
        }

        protected void ValidateClientName()
        {
            RuleFor(c => c.Client.ClientName).NotEmpty().WithMessage("Client Name must be set");
        }

    }
}