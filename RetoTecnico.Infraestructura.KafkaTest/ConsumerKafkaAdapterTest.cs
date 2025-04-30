using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Common.Test.Base;
using RetoTecnico.Common.Test.Seeds;
using RetoTecnico.Infraestructura.Kafka.Adapter;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

namespace RetoTecnico.Infraestructura.KafkaTest;

public class ConsumerKafkaAdapterTest : BaseTest
{
    private readonly IConfiguration _mockConfiguration;
    private readonly IServiceScopeFactory _mockServiceScopeFactory;

    public ConsumerKafkaAdapterTest()
    {
        _mockConfiguration = Substitute.For<IConfiguration>();
        _mockServiceScopeFactory = Substitute.For<IServiceScopeFactory>();
    }

    [Fact]
    public async Task Consumer_Success()
    {
        var mockedProvider = Substitute.For<IServiceProvider>();
        var database = nameof(Consumer_Success);
        var mockedContext = await GetMockContext(database);
        mockedProvider.GetService<NpgsqlContext>().Returns(mockedContext);

        var repository = new TransactionRepository(mockedProvider);
        mockedProvider.GetService<ITransactionRepository>().Returns(repository);

        await mockedContext.SeedTestingTransactionsAsync();
        SeedTestingTransactions.SetupMockRepositories(mockedProvider);

        var adapter = new ConsumerKafkaAdapter(_mockServiceScopeFactory, _mockConfiguration);

        adapter.Consumer = Substitute.For<IConsumer<Ignore, string>>();

        await adapter.ExecuteTask(new CancellationToken());
    }
}