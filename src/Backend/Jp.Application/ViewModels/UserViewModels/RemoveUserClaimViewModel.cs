using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class RemoveUserClaimViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Username { get; set; }
    }
}