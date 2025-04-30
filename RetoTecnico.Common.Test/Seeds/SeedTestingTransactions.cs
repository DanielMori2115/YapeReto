using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

namespace RetoTecnico.Common.Test.Seeds;

public static class SeedTestingTransactions
{
    public static async Task SeedTestingTransactionsAsync(this NpgsqlContext dbContext)
    {
        var transactions = new List<Transaction>
            {
                new()
                {
                    TransactionId = 1,
                    Value = 11
                },
                new()
                {
                    TransactionId = 2,
                    Value = 12
                },
                new()
                {
                    TransactionId = 3,
                    Value = 2500
                }
            };

        await dbContext.Transactions.AddRangeAsync(transactions);
        await dbContext.SaveChangesAsync();
    }

    public static void SetupMockRepositories(IServiceProvider mockedProvider)
    {
        var transactionRepository = new TransactionRepository(mockedProvider);
        mockedProvider.GetService<TransactionRepository>().Returns(transactionRepository);
    }
}
