using FluentValidation;
using IdentityServer4.Models;
using Jp.Domain.Commands.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jp.Domain.Validations.Client
{
    public abstract class ClientValidation<T> : AbstractValidator<T> where T : ClientCommand
    {
        protected void ValidateGrantType()
        {
            var message = "Invalid grant types";
            RuleFor(c => c.Client.AllowedGrantTypes)
                .NotEmpty().WithMessage("At Least 1 grant type must be selected")
                .Must(m => ValidateGrantCombination(m, message)).WithMessage(message);

        }

        private bool ValidateGrantCombination(ICollection<string> grantTypes, string message)
        {

            // would allow response_type downgrade attack from code to token
            if (DisallowGrantTypeCombination(GrantType.Implicit, GrantType.AuthorizationCode, grantTypes))
            {
                message = string.Format(message, GrantType.Implicit, GrantType.AuthorizationCode);
                return false;
            }

            if (DisallowGrantTypeCombination(GrantType.Implicit, GrantType.Hybrid, grantTypes))
            {
                message = string.Format(message, GrantType.Implicit, GrantType.Hybrid);
                return false;
            }

            if (DisallowGrantTypeCombination(GrantType.AuthorizationCode, GrantType.Hybrid, grantTypes))
            {
                message = string.Format(message, GrantType.AuthorizationCode, GrantType.Hybrid);
                return false;
            }

            return true;
        }
        private bool DisallowGrantTypeCombination(string value1, string value2, IEnumerable<string> grantTypes)
        {
            return grantTypes.Contains(value1, StringComparer.Ordinal) &&
                   grantTypes.Contains(value2, StringComparer.Ordinal);
        }

        protected void ValidateClientId()
        {
            RuleFor(c => c.Client.ClientId).NotEmpty().WithMessage("ClientId must be set");
        }

        protected void ValidateOldClientId()
        {
            RuleFor(c => c.OldClientId).NotEmpty().WithMessage("Last ClientId must be set");
        }

        protected void ValidateClientName()
        {
            RuleFor(c => c.Client.ClientName).NotEmpty().WithMessage("Client Name must be set");
        }

        protected void ValidateIdentityTokenLifetime()
        {
            RuleFor(c => c.Client.IdentityTokenLifetime).GreaterThan(0).WithMessage("Identity Token Lifetime must be greatter than 0");
        }


        protected void ValidateAccessTokenLifetime()
        {
            RuleFor(c => c.Client.AccessTokenLifetime).GreaterThan(0).WithMessage("Access Token Lifetime must be greatter than 0");
        }
        protected void ValidateAuthorizationCodeLifetime()
        {
            RuleFor(c => c.Client.AuthorizationCodeLifetime).GreaterThan(0).WithMessage("Authorization Code Lifetime must be greatter than 0");
        }
        protected void ValidateAbsoluteRefreshTokenLifetime()
        {
            RuleFor(c => c.Client.AbsoluteRefreshTokenLifetime).GreaterThan(0).WithMessage("Absolute Refresh Token Lifetime must be greatter than 0");
        }
        protected void ValidateSlidingRefreshTokenLifetime()
        {
            RuleFor(c => c.Client.SlidingRefreshTokenLifetime).GreaterThan(0).WithMessage("Sliding Refresh Token Lifetime must be greatter than 0");
        }
        protected void ValidateDeviceCodeLifetime()
        {
            RuleFor(c => c.Client.DeviceCodeLifetime).GreaterThan(0).WithMessage("Device Code Lifetime must be greatter than 0");
        }
    }
}