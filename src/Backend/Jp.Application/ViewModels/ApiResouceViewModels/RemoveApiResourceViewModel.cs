using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.IdentityResourceViewModels
{
    public class RemoveApiResourceViewModel
    {
        [Required]
        public string Name { get; set; }
    }

}