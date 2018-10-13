using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels
{
    public class RemovePersistedGrantViewModel
    {
        [Required]
        public string Key { get; set; }
    }
}