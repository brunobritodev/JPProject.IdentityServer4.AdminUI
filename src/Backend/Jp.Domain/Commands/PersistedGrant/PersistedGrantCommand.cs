using System;
using System.Collections.Generic;
using System.Text;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.PersistedGrant
{
    public abstract class PersistedGrantCommand : Command
    {
        public string Key { get; protected set; }
    }
}
