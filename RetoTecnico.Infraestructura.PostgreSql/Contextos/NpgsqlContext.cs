using Microsoft.EntityFrameworkCore;

using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Configs;

namespace RetoTecnico.Infraestructura.PostgreSql.Contextos;

public class NpgsqlContext(DbContextOptions<NpgsqlContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TransactionConfig());
    }
}
