using System;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class RemoveUserRoleViewModel
    {
        [Required]
        public string Role { get; set; }
        [Required]
        public string Username { get; set; }

        public Guid UserId { get; set; }
    }
}