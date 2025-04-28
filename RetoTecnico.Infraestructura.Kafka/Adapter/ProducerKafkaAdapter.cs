using Confluent.Kafka;

using System.Diagnostics;
using System.Net;

using RetoTecnico.Aplicacion.Base;
using RetoTecnico.Aplicacion.Interfaces;

namespace RetoTecnico.Infraestructura.Kafka.Adapter;

public class ProducerKafkaAdapter : IProducerKafkaAdapter
{
    public async Task<bool> ExecuteAsync(TransactionBaseRequest transactionBaseRequest)
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
