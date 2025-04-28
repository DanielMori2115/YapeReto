using RetoTecnico.Aplicacion.Dto;

namespace RetoTecnico.Aplicacion.Interfaces.Service;

public interface ITransactionService : IServicioBase<TransactionDto, long>
{
    Task<bool> ProcessTransaction(TransactionDto transactionDto, string server, string topic);
}