using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands.User
{
    public abstract class UserCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Name { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
