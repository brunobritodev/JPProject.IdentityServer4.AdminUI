using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Equinox.UI.SSO.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.SSO.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

    }
}
