using Microsoft.AspNetCore.Mvc;

using RetoTecnico.Aplicacion.Dto;
using RetoTecnico.Aplicacion.Interfaces.Service;
using RetoTecnico.Dominio.Enum;

namespace RetoTecnico.Infraestructura.TransactionAPI.Adapter
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionAdapter(IConfiguration configuration, ITransactionService transactionService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTransactionDto addTransactionDto)
        {
            var server = configuration["Kafka:BootstrapServers"];
            var topic = configuration["Kafka:Topic"];

            var transactionDto = new TransactionDto
            {
                Value = addTransactionDto.Value,
                SourceAccountId = addTransactionDto.SourceAccountId,
                TargetAccountId = addTransactionDto.TargetAccountId,
                TransactionDate = DateTime.Now.ToUniversalTime(),
                TransactionTypeId = (int)TransactionTypeEnum.Pending
            };

            var pendingTransaction = transactionService.Agregar(transactionDto);

            await transactionService.ProcessTransaction(pendingTransaction, server, topic);

            var transaction = transactionService.SeleccionarPorID(pendingTransaction.TransactionId);
            var response = new AddTransactionResponseDto
            {
                CreatedAt = transaction.TransactionDate,
                TransactionExternalId = transaction.TransactionId.ToString()
            };

            return Ok(response);
        }
    }
}
