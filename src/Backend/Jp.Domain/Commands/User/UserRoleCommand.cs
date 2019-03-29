using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.User
{
    public abstract class UserRoleCommand : Command
    {
        public string Username { get; protected set; }
        public string Role { get; protected set; }
    }
}