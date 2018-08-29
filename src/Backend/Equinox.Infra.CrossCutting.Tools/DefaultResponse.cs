using System.Collections.Generic;
using Equinox.Domain.Core.Notifications;

namespace Equinox.Infra.CrossCutting.Tools
{
    public class DefaultResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
}
}
