using System;
using FluentValidation;
using Jp.Domain.Commands.IdentityResources;

namespace Jp.Domain.Validations.IdentityResources
{
    public abstract class IdentityResourcesValidation<T> : AbstractValidator<T> where T : IdentityResourcesCommand
    {
        
    }
}