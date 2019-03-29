using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ClientClaimCommand : Command
    {
        public int Id { get; protected set; }
        public string ClientId { get; protected set; }
        public string Type { get; protected set; }

        public string Value { get; protected set; }
    }
}