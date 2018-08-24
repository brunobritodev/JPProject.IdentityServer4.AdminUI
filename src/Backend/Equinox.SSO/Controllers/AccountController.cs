using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.Infra.CrossCutting.Identity.Models.AccountViewModels;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.SSO.Controllers
{
    [Route("[controller]")]
    public class AccountController : ApiController
    {
        private readonly IUserManagerAppService _userManagerAppService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IUserManagerAppService userManagerAppService,
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events) : base(notifications, mediator)
        {
            _userManagerAppService = userManagerAppService;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

       

        /// <summary>
        /// Get login info to know what to sho on the login page page
        /// </summary>
        [HttpGet, Route("login-info")]
        public async Task<IActionResult> Login()
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync();

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return await ExternalLogin(vm.ExternalLoginScheme);
            }

            return Response(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            if (button != "login")
            {
                // the user clicked the "cancel" button
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                //if (result.Succeeded)
                //{
                //    var user = await _userManager.FindByNameAsync(model.Username);
                //    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));

                //    // make sure the returnUrl is still valid, and if so redirect back to authorize endpoint or a local page
                //    // the IsLocalUrl check is only necessary if you want to support additional local pages, otherwise IsValidReturnUrl is more strict
                //    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                //    {
                //        return Redirect(model.ReturnUrl);
                //    }

                //    return Redirect("~/");
                //}

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            NotifyModelStateErrors();
            var vm = await BuildLoginViewModelAsync(model);
            return Response(model);
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider)
        {
            if (AccountOptions.WindowsAuthenticationSchemeName == provider)
            {
                // windows authentication needs special handling
                return await ProcessWindowsLoginAsync();
            }
            else
            {
                // start challenge and roundtrip the return URL and 
                var props = new AuthenticationProperties()
                {
                    RedirectUri = "ExternalLoginCallback",
                    Items =
                    {
                        { "scheme", provider },
                    }
                };
                return Challenge(props, provider);
            }
        }

        private async Task<IActionResult> ProcessWindowsLoginAsync()
        {
            // see if windows auth has already been requested and succeeded
            var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
            if (result?.Principal is WindowsPrincipal wp)
            {
                // we will issue the external cookie and then redirect the
                // user back to the external callback, in essence, tresting windows
                // auth the same as any other external authentication mechanism
                var props = new AuthenticationProperties()
                {
                    RedirectUri = "ExternalLoginCallback",
                    Items =
                    {
                        { "scheme", AccountOptions.WindowsAuthenticationSchemeName },
                    }
                };

                var id = new ClaimsIdentity(AccountOptions.WindowsAuthenticationSchemeName);
                id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

                // add the groups as claims -- be careful if the number of groups is too large
                if (AccountOptions.IncludeWindowsGroups)
                {
                    var wi = wp.Identity as WindowsIdentity;
                    var groups = wi.Groups.Translate(typeof(NTAccount));
                    var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                    id.AddClaims(roles);
                }

                await HttpContext.SignInAsync(
                    IdentityConstants.ExternalScheme,
                    new ClaimsPrincipal(id),
                    props);
                return Redirect(props.RedirectUri);
            }
            else
            {
                // trigger windows auth
                // since windows auth don't support the redirect uri,
                // this URL is re-triggered when we call challenge
                return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
            }
        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync()
        {
            var context = await _interaction.GetAuthorizationContextAsync(string.Empty);
            if (context?.IdP != null)
            {
                // this is meant to short circuit the UI and only trigger the one external IdP
                return new LoginViewModel
                {
                    EnableLocalLogin = false,
                    Username = context?.LoginHint,
                    ExternalProviders = new ExternalProviderViewModel[] { new ExternalProviderViewModel { AuthenticationScheme = context.IdP } }
                };
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProviderViewModel
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync();
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }
    }
}