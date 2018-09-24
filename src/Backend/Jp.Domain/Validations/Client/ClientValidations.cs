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
    }
}