using RetoTecnico.Aplicacion.Base;

namespace RetoTecnico.Aplicacion.Interfaces;

public interface IProducerKafkaAdapter
{
    Task<bool> ExecuteAsync(TransactionBaseRequest transactionBaseRequest);
}
