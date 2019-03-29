using System;
using System.Collections.Generic;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ApiScopeCommand : Command
    {
        public int Id { get; protected set; }
        public string ResourceName { get; protected set; }
        /// <summary>
        /// Name of the scope. This is the value a client will use to request the scope.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Display name. This value will be used e.g. on the consent screen.
        /// </summary>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// Description. This value will be used e.g. on the consent screen.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
        /// </summary>
        public bool Required { get; protected set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; protected set; } = false;

        /// <summary>
        /// Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        public bool ShowInDiscoveryDocument { get; protected set; } = true;

        public IEnumerable<string> UserClaims { get; protected set; } = new HashSet<string>();
    }
}