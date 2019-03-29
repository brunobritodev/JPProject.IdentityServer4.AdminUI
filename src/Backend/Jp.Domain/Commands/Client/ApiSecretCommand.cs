using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ApiSecretCommand : Command
    {
        public int Id { get; protected set; }
        public string ResourceName { get; protected set; }
        public string Description { get; protected set; }

        public string Value { get; protected set; }
        public DateTime? Expiration { get; protected set; }

        public int Hash { get; protected set; } = 0;
        public string Type { get; protected set; }
    }
}