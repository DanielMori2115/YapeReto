﻿using Confluent.Kafka;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RetoTecnico.Aplicacion.Interfaces.Service;

namespace RetoTecnico.Infraestructura.Kafka.Adapter;

public class ConsumerKafkaAdapter : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly IConsumer<Ignore, string> _consumer;

    public ConsumerKafkaAdapter(IServiceScopeFactory  scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = "KafkaGroupId"
        };

        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var topic = _configuration["Kafka:Topic"];

        _consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessKafkaMessage(stoppingToken);

            Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _consumer.Close();
    }

    private void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);
            var message = consumeResult.Message.Value.Split("::transactionId::");

            var existsTransactionId = int.TryParse(message.LastOrDefault(), out var transactionId);

            if (!existsTransactionId) return;

            using (var scope = _scopeFactory.CreateScope())
            {
                var transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();

                transactionService.ValidateTransaction(transactionId);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
