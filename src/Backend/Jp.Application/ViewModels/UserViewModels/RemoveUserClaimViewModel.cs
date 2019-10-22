using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class RemoveUserClaimViewModel
    {
        public RemoveUserClaimViewModel(string type, string value)
        {
            Type = type;
            Value = value;
        }

        [Required]
        public string Type { get; set; }
        [Required]
        public string Username { get; set; }

        public string Value { get; set; }
    }
}