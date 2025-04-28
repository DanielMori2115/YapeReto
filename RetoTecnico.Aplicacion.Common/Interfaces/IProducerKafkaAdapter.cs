using RetoTecnico.Aplicacion.Common.Base;

namespace RetoTecnico.Aplicacion.Common.Interfaces;

public interface IProducerKafkaAdapter
{
    Task<bool> ExecuteAsync(TransactionBaseRequest transactionBaseRequest);
}
