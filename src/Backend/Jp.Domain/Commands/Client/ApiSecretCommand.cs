using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ApiSecretCommand : Command
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string Description { get; set; }

        public string Value { get; set; }
        public DateTime? Expiration { get; set; }

        public int Hash { get; set; } = 0;
        public string Type { get; set; }
    }
}