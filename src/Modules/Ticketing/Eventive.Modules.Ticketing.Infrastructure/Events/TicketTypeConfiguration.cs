using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Eventive.Modules.Ticketing.Domain.Events;

namespace Eventive.Modules.Ticketing.Infrastructure.Events;

internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
