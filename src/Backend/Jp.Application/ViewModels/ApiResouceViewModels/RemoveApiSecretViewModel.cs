using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{

    public class RemoveApiSecretViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ResourceName { get; set; }
    }
}
