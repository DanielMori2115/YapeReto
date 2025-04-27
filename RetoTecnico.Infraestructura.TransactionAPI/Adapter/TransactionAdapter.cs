using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using RetoTecnico.Aplicacion.Transaction.Dto;
using RetoTecnico.Aplicacion.Transaction.Interfaces;
using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Infraestructura.TransactionAPI.Adapter
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionAdapter(IConfiguration configuration, ITransactionProducerService transactionProducerService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTransactionDto addTransactionDto)
        {
            var valueToValidate = JsonSerializer.Serialize(addTransactionDto.Value);
            
            var t = new Transaction
            {
                Value = addTransactionDto.Value,
                SourceAccountId = addTransactionDto.SourceAccountId,
                TargetAccountId = addTransactionDto.TargetAccountId,
                TransactionDate = DateTime.Now
            };

            var request = new TransactionBaseRequest
            {
                Message = valueToValidate,
                Server = configuration["Kafka:BootstrapServers"],
                Topic = configuration["Kafka:Topic"],
            };

            var test = await transactionProducerService.ValidateTransaction(t, request);

            return Ok(test);
        }
    }
}
