using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using RetoTecnico.Aplicacion.CasoUso;
using RetoTecnico.Aplicacion.Interfaces;
using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Common.Test.Base;
using RetoTecnico.Common.Test.Seeds;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;
using RetoTecnico.Dominio.Enum;

namespace RetoTecnico.Aplicacion.Test;

public class TransactionServiceTest : BaseTest
{
    private readonly IProducerKafkaAdapter _producerKafkaAdapter;

    public TransactionServiceTest()
    {
        InitMapper();
        _producerKafkaAdapter = Substitute.For<IProducerKafkaAdapter>();
    }

    [Fact]
    public async Task ValidateTransaction_Approved()
    {
        //Arrange
        const int transactionIdToValidate = 1;

        var mockedProvider = Substitute.For<IServiceProvider>();
        var database = nameof(ValidateTransaction_Approved);

        var mockedContext = await GetMockContext(database);
        mockedProvider.GetService<NpgsqlContext>().Returns(mockedContext);

        var repository = new TransactionRepository(mockedProvider);
        mockedProvider.GetService<ITransactionRepository>().Returns(repository);

        await mockedContext.SeedTestingTransactionsAsync();
        SeedTestingTransactions.SetupMockRepositories(mockedProvider);

        //Act
        var adapter = new TransactionService(Mapper, _producerKafkaAdapter, repository);

        adapter.ValidateTransaction(transactionIdToValidate);
        var transactionUpdated = adapter.SeleccionarPorID(transactionIdToValidate);

        //Assert
        Assert.Equal(transactionUpdated.TransactionTypeId, (int)TransactionTypeEnum.Approved);
        await mockedContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task ValidateTransaction_Rejected()
    {
        //Arrange
        const int transactionIdToValidate = 3;

        var mockedProvider = Substitute.For<IServiceProvider>();
        var database = nameof(ValidateTransaction_Rejected);

        var mockedContext = await GetMockContext(database);
        mockedProvider.GetService<NpgsqlContext>().Returns(mockedContext);

        var repository = new TransactionRepository(mockedProvider);
        mockedProvider.GetService<ITransactionRepository>().Returns(repository);

        await mockedContext.SeedTestingTransactionsAsync();
        SeedTestingTransactions.SetupMockRepositories(mockedProvider);

        //Act
        var adapter = new TransactionService(Mapper, _producerKafkaAdapter, repository);

        adapter.ValidateTransaction(transactionIdToValidate);
        var transactionUpdated = adapter.SeleccionarPorID(transactionIdToValidate);

        //Assert
        Assert.Equal(transactionUpdated.TransactionTypeId, (int)TransactionTypeEnum.Rejected);
        await mockedContext.Database.EnsureDeletedAsync();
    }
}
