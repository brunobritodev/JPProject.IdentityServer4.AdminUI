using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class RemoveApiScopeViewModel
    {
        public RemoveApiScopeViewModel(string resourceName, int id)
        {
            ResourceName = resourceName;
            Id = id;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string ResourceName { get; set; }
    }
}