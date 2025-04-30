using AutoMapper;

using RetoTecnico.Aplicacion.Base;
using RetoTecnico.Aplicacion.Dto;
using RetoTecnico.Aplicacion.Interfaces;
using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Aplicacion.Interfaces.Service;
using RetoTecnico.Aplicacion.Util;
using RetoTecnico.Dominio.Enum;
using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Aplicacion.CasoUso;

public class TransactionService(
    IMapper mapper, 
    IProducerKafkaAdapter producerKafkaAdapter,
    ITransactionRepository transactionRepository)
    : ITransactionService
{
    private IMapper Mapper { get; } = mapper;

    public TransactionDto Agregar(TransactionDto entidad)
    {
        transactionRepository.BeginTransaction();

        try
        {
            var transactionToAdd = Mapper.Map<Transaction>(entidad);
            var newTransaction = transactionRepository.Agregar(transactionToAdd);

            transactionRepository.GuardarTodosLosCambios();
            transactionRepository.CommitTransaction();

            return Mapper.Map<TransactionDto>(newTransaction);
        }
        catch (Exception e)
        {
            transactionRepository.RollbackTransaction();
            throw;
        }
    }

    public void Editar(TransactionDto entidad)
    {
        transactionRepository.BeginTransaction();

        try
        {
            var model = Mapper.Map<Transaction>(entidad);

            transactionRepository.Editar(model);

            transactionRepository.GuardarTodosLosCambios();
            transactionRepository.CommitTransaction();
        }
        catch (Exception e)
        {
            transactionRepository.RollbackTransaction();
            throw;
        }
    }

    public void Eliminar(long entidadId)
    {
        throw new NotImplementedException();
    }

    public List<TransactionDto> Listar()
    {
        var transactions = transactionRepository.Listar();

        return Mapper.Map<List<TransactionDto>>(transactions);
    }

    public TransactionDto SeleccionarPorID(long entidadId)
    {
        var transactions = transactionRepository.SeleccionarPorID(entidadId);

        return Mapper.Map<TransactionDto>(transactions);
    }

    public async Task<bool> ProcessTransaction(TransactionDto transactionDto, string server, string topic)
    {
        var valueToValidate = transactionDto.Value.ToJson();

        var request = new TransactionBaseRequest
        {
            Message = $"{valueToValidate}::transactionId::{transactionDto.TransactionId}",
            Server = server,
            Topic = topic,
        };

        var response = await producerKafkaAdapter.ExecuteAsync(request);

        return await Task.FromResult(response);
    }

   public void ValidateTransaction(int transactionId)
    {
        const int maxTransactionValue = 2000;

        var transaction = SeleccionarPorID(transactionId);

        transaction.TransactionTypeId = transaction.Value > maxTransactionValue ? (int)TransactionTypeEnum.Rejected : (int)TransactionTypeEnum.Approved;

        Editar(transaction);
    }
}
