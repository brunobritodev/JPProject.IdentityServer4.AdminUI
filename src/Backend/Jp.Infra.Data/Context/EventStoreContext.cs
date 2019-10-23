using Jp.Domain.Core.Events;
using Jp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Jp.Infra.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }
        public DbSet<EventDetails> StoredEventDetails { get; set; }
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            modelBuilder.ApplyConfiguration(new EventDetailsMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
