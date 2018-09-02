using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class RegisterNewUserCommand : UserCommand
    {
        public RegisterNewUserCommand(string username, string email, string name, string phoneNumber, string password, string confirmPassword)
        {
           
            Username = username;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Password = password;
            ConfirmPassword = confirmPassword;
            
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
