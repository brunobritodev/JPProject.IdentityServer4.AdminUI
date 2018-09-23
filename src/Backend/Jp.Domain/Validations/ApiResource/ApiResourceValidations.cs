using System;
using FluentValidation;
using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public abstract class ApiResourceValidation<T> : AbstractValidator<T> where T : ApiResourceCommand
    {
        
    }
}