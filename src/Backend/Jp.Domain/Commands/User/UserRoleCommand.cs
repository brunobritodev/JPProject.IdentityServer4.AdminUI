using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.User
{
    public abstract class UserRoleCommand : Command
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}