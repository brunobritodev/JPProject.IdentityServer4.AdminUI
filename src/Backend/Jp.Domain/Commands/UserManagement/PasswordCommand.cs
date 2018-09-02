using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.UserManagement
{
    public abstract class PasswordCommand : Command
    {
        public Guid? Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
    }
}