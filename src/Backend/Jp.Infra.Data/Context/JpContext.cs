using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jp.Infra.Data.Context
{
    public class JpContext : DbContext
    {
        public JpContext(DbContextOptions<JpContext> options
        ) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }
    }
    
}
