using System.IO;
using Jp.Domain.Core.Events;
using Jp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jp.Infra.Data.Context
{
    public class EventStoreSQLContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }
        public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}