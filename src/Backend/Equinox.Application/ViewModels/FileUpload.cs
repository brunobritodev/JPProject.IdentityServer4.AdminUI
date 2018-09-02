using System;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels
{
    public class ProfilePictureViewModel
    {
        [Required(ErrorMessage = "Invalid image")]
        public string Filename { get; set; }
        [Required(ErrorMessage = "Invalid image")]
        public string FileType { get; set; }
        [Required(ErrorMessage = "Invalid image")]
        public string Value { get; set; }

        public Guid? Id { get; set; }
    }
}
