using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jp.UI.SSO.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IIdentityServerInteractionService interaction, IHostingEnvironment hostingEnvironment)
        {
            _interaction = interaction;
            _hostingEnvironment = hostingEnvironment;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();
            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
                vm.ErrorMessage = vm?.Error?.Error;
                vm.ErrorDescription = User.IsInRole("Administrator") || _hostingEnvironment.IsDevelopment() ? vm?.Error?.ErrorDescription : null;
                vm.RequestId = vm?.Error?.RequestId;
            }

            return View("Error", vm);
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public IActionResult LoginError(string error)
        {
            var vm = new ErrorViewModel();

            if (error != null)
            {
                vm.Error = new ErrorMessage() { ErrorDescription = error, Error = "1000" };
                vm.ErrorMessage = vm?.Error?.Error;
                vm.ErrorDescription = User.IsInRole("Administrator") ? vm?.Error?.ErrorDescription : null;
                vm.RequestId = vm?.Error?.RequestId;
            }

            return View("Error", vm);
        }


        [HttpGet]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("Index");

        }
    }
}