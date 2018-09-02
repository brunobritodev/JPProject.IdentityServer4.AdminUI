using System;
using System.IO;
using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.WebApi.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using ChangePasswordViewModel = Equinox.Infra.CrossCutting.Identity.Models.ManageViewModels.ChangePasswordViewModel;

namespace Equinox.WebApi.Controllers
{
    [Authorize]
    public class ManageController : ApiController
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IImageStorage _imageStorage;

        public ManageController(
            INotificationHandler<DomainNotification> notifications,
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager,
            IConfiguration configuration,
            IMediatorHandler mediator,
            IImageStorage imageStorage) : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _imageStorage = imageStorage;
        }

        [HttpPost]
        [Route("account-management/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load userIdentity with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return Response(changePasswordResult);
        }


        [HttpPost]
        [Route("account-management/update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfile model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load userIdentity with ID '{_userManager.GetUserId(User)}'.");
            }

            user.Company = model.Company;
            user.Bio = model.Bio;
            user.Name = model.Name;
            user.Url = model.UserName;
            user.JobTitle = model.JobTitle;

            var result = await _userManager.UpdateAsync(user);
            return Response(result);
        }


        [HttpPost]
        [Route("account-management/update-picture")]
        public async Task<IActionResult> UploadFile([FromBody] ProfilePictureViewModel file)
        {
            if (!file.FileType.Contains("image"))
            {
                NotifyError("Type", "Invalid filetype");
                return Response();
            }

            var user = await _userManager.GetUserAsync(User);
            user.Picture = await _imageStorage.SaveAsync(file);
            await _userManager.UpdateAsync(user);

            return Response(user.Picture);
        }

       


        [HttpGet]
        [Route("account-management/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            return Response(new UserProfile(user));
        }

    }
}
