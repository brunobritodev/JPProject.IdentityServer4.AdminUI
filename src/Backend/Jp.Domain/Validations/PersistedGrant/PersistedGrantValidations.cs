using System;
using FluentValidation;
using Jp.Domain.Commands.PersistedGrant;

namespace Jp.Domain.Validations.PersistedGrant
{
    public abstract class PersistedGrantValidation<T> : AbstractValidator<T> where T : PersistedGrantCommand
    {
        protected void ValidateKey()
        {
            RuleFor(c => c.Key).NotEmpty();
        }
    }
}