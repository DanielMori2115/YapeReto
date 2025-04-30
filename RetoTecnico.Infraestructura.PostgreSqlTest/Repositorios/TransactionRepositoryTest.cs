using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Common.Test.Base;
using RetoTecnico.Common.Test.Seeds;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

namespace RetoTecnico.Infraestructura.PostgreSqlTest.Repositorios
{
    public class TransactionRepositoryTest : BaseTest
    {
        [Fact]
        public async Task Agregar_Success()
        {
            var mockedProvider = Substitute.For<IServiceProvider>();
            var database = nameof(Agregar_Success);
            var mockedContext = await GetMockContext(database);
            mockedProvider.GetService<NpgsqlContext>().Returns(mockedContext);

            var repository = new TransactionRepository(mockedProvider);
            mockedProvider.GetService<ITransactionRepository>().Returns(repository);

            await mockedContext.SeedTestingTransactionsAsync();
            SeedTestingTransactions.SetupMockRepositories(mockedProvider);


        }
    }
}