using System;

namespace Jp.Domain.Models
{
    public class Role
    {
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
