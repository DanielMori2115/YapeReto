using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Infraestructura.PostgreSql.Configs
{
    public class TransactionConfig: IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(c => c.TransactionId);
            
            builder.Property(c => c.TransactionId).HasColumnName("Transaction_Id");
            builder.Property(e => e.SourceAccountId).HasColumnName("Source_Account_Id");
            builder.Property(e => e.TargetAccountId).HasColumnName("Target_Account_Id");
            builder.Property(e => e.TransactionDate).HasColumnName("Transaction_Date");
            builder.Property(e => e.TransactionTypeId).HasColumnName("Transaction_Type_Id");
            builder.Property(e => e.Value).HasColumnName("Value");
        }
    }
}
