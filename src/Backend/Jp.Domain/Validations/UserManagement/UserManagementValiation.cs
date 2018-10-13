using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
{
    public abstract class UserManagementValidation<T> : AbstractValidator<T> where T : UserManagementCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Username")
                .Length(2, 150).WithMessage("The Username must have between 2 and 150 characters");
        }


        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }


        protected void ValidateUsername()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Please ensure you have entered the Username")
                .Length(2, 50).WithMessage("The Username must have between 2 and 50 characters");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Please ensure you have entered the password")
                .Equal(c => c.ConfirmPassword).WithMessage("Password and Confirm password must be equal")
                .MinimumLength(8).WithMessage("Password minimun length must be 8 characters");
        }

        protected void ValidateProvider()
        {
            RuleFor(c => c.Provider)
                .NotEmpty();
        }

        protected void ValidateProviderId()
        {
            RuleFor(c => c.ProviderId)
                .NotEmpty();
        }
    }
}
