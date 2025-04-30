using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;

using RetoTecnico.Aplicacion.Dto;
using RetoTecnico.Aplicacion.Interfaces.Service;
using RetoTecnico.Infraestructura.TransactionAPI.Adapter;

namespace RetoTecnico.Infraestructura.TransactionAPITest;

public class TransactionAdapterTest
{
    private readonly IConfiguration _mockConfiguration;
    private readonly ITransactionService _mockTransactionService;
    private TransactionAdapter _adapter;

    public TransactionAdapterTest()
    {
        _mockConfiguration = Substitute.For<IConfiguration>();
        _mockTransactionService = Substitute.For<ITransactionService>();
    }

    [Fact]
    public async Task AddTransaction_SuccessRequest()
    {
        // Arrange
        const long transactionId = 111;

        var request = new AddTransactionDto();
        var responseDto = new TransactionDto { TransactionId = transactionId };
        var response = new AddTransactionResponseDto { TransactionExternalId = transactionId.ToString() };

        _mockTransactionService.Agregar(Arg.Any<TransactionDto>()).Returns(responseDto);
        _mockTransactionService.SeleccionarPorID(Arg.Any<long>()).Returns(responseDto);
        _adapter = new TransactionAdapter(_mockConfiguration, _mockTransactionService);

        // Act
        var result = await _adapter.Post(request);

        // Assert
        Assert.Equal(response.TransactionExternalId, responseDto.TransactionId.ToString());

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<AddTransactionResponseDto>(okResult.Value);
    }
}