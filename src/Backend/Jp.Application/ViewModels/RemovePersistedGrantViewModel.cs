using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels
{
    public class RemovePersistedGrantViewModel
    {
        public RemovePersistedGrantViewModel(string key)
        {
            Key = key;
        }

        [Required]
        public string Key { get; set; }
    }
}