using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Jp.Domain.Commands.Role;

namespace Jp.Domain.Validations.Role
{
    public abstract class RoleValidation<T> : AbstractValidator<T> where T : RoleCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
