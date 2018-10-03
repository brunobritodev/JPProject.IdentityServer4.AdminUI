using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.User
{
    public abstract class UserClaimCommand : Command
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }

        public string Value { get; set; }
    }
}