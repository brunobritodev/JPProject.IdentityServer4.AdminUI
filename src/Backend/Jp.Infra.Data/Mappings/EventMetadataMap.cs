using Jp.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jp.Infra.Data.Mappings
{
    public class EventDetailsMap : IEntityTypeConfiguration<EventDetails>
    {
        public void Configure(EntityTypeBuilder<EventDetails> builder)
        {
            builder.HasKey(k => k.EventId);
            builder.HasOne(o => o.Event).WithOne(o => o.Details).HasForeignKey<EventDetails>(fk => fk.EventId);
        }
    }
}