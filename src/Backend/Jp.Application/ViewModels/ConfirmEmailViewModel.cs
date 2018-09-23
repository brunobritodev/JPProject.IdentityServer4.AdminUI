using System.ComponentModel.DataAnnotations;
using Jp.Domain.Core.Models;

namespace Jp.Application.ViewModels
{
    public class ConfirmEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }

    }
}
