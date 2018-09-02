using System.Collections.Generic;

namespace Jp.Infra.CrossCutting.Tools.Model
{
    public class DefaultResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
}
}
