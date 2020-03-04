using JPProject.Domain.Core.Events;
using JPProject.EntityFrameworkCore.Interfaces;
using JPProject.EntityFrameworkCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Jp.Database.Context
{
    public class EventStoreContext : DbContext, IEventStoreContext
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

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}