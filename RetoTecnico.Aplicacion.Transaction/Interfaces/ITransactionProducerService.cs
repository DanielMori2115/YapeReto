using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Aplicacion.Transaction.Interfaces;

public interface ITransactionProducerService
{
    Task<bool> ValidateTransaction(Dominio.Models.Transaction transactionRequest, TransactionBaseRequest transactionBaseRequest);
}