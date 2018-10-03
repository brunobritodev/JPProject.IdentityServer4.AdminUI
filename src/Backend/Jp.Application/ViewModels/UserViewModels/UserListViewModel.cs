using System;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class UserListViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Name { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public Guid? Id { get; set; }
    }
}