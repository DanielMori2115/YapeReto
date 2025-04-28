using Microsoft.AspNetCore.Mvc;

using RetoTecnico.Aplicacion.Transaction.Dto;
using RetoTecnico.Aplicacion.Transaction.Interfaces;

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

            var isValid = await transactionService.ValidateTransaction(addTransactionDto, server, topic);

            if (!isValid) throw new BadHttpRequestException("Lo sentimos, no se pudo procesar su solicitud");

            var transactionDto = new TransactionDto
            {
                Value = addTransactionDto.Value,
                SourceAccountId = addTransactionDto.SourceAccountId,
                TargetAccountId = addTransactionDto.TargetAccountId,
                TransactionDate = DateTime.Now
            };

            var newTransaction = transactionService.Agregar(transactionDto);

            return Ok(newTransaction != null);
        }
    }
}
