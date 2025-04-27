using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Infraestructura.PostgreSql.Configs
{
    public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("Outbox_Message");
            builder.HasKey(c => c.EventId);

            builder.Property(c => c.EventId).HasColumnName("Event_Id");
            builder.Property(e => e.EventPayload).HasColumnName("Event_Payload");
            builder.Property(e => e.EventDate).HasColumnName("Event_Date");
            builder.Property(e => e.IsMessageDispatched).HasColumnName("IsMessageDispatched");
        }
    }
}
