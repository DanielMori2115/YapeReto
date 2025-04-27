using System.Diagnostics;
using System.Net;

using Confluent.Kafka;

using RetoTecnico.Aplicacion.Transaction.Interfaces;
using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Aplicacion.Transaction.CasoUso
{
    public class TransactionProducerService : ITransactionProducerService
    {
        public async Task<bool> ValidateTransaction(Dominio.Models.Transaction transactionRequest, TransactionBaseRequest transactionBaseRequest)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = transactionBaseRequest.Server,
                ClientId = Dns.GetHostName()
            };

            try
            {
                var transactionValue = transactionBaseRequest.Message;

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(transactionBaseRequest.Topic, new Message<Null, string>
                    {
                        Value = transactionValue
                    });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
