using RetoTecnico.Aplicacion.Common.Interfaces;
using RetoTecnico.Aplicacion.Transaction.Dto;

namespace RetoTecnico.Aplicacion.Transaction.Interfaces;

public interface ITransactionService : IServicioBase<TransactionDto, long>
{
    Task<bool> ValidateTransaction(AddTransactionDto addTransactionDto, string server, string topic);
}