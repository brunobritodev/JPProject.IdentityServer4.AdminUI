using System;
using Jp.Domain.Validations.UserManagement;

namespace Jp.Domain.Commands.UserManagement
{
    public class UpdateProfileCommand : ProfileCommand
    {

        public UpdateProfileCommand(Guid? id, string url, string bio, string company, string jobTitle, string name, string phoneNumber)
        {
            Id = id;
            Url = url;
            Bio = bio;
            Company = company;
            JobTitle = jobTitle;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProfileCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
    
}
