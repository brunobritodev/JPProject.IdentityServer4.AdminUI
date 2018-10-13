using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.IdentityResourceViewModels
{
    public class RemoveIdentityResourceViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
