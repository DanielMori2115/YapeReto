using Microsoft.EntityFrameworkCore;
using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Configs;

namespace RetoTecnico.Infraestructura.PostgreSql.Contextos;

public class TransactionContext(DbContextOptions<TransactionContext> options) : DbContext(options)
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new OutboxMessageConfig());
        builder.ApplyConfiguration(new TransactionConfig());
    }
}