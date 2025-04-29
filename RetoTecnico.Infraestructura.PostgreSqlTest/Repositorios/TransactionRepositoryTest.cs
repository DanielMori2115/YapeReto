using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using RetoTecnico.Aplicacion.Interfaces.Repository;

using RetoTecnico.Common.Test;

using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

namespace RetoTecnico.Infraestructura.PostgreSqlTest.Repositorios
{
    public class TransactionRepositoryTest : BaseTest
    {
        [Fact]
        public async Task Test1()
        {
            var mockedProvider = Substitute.For<IServiceProvider>();
            var database = nameof(Test1);
            var mockedContext = await GetMockContext(database);
            mockedProvider.GetService<NpgsqlContext>().Returns(mockedContext);

            var repository = new TransactionRepository(mockedProvider);
            mockedProvider.GetService<ITransactionRepository>().Returns(repository);
        }
    }
}