using System;
using System.Collections.Generic;
using System.Text;

namespace Equinox.Domain.Interfaces
{
    public interface ISerializer
    {
        T DeserializeFromString<T>(string value);
    }
}
