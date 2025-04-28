using AutoMapper;

using RetoTecnico.Aplicacion.Common.Base;
using RetoTecnico.Aplicacion.Common.Interfaces;
using RetoTecnico.Aplicacion.Common.Util;
using RetoTecnico.Aplicacion.Transaction.Dto;
using RetoTecnico.Aplicacion.Transaction.Interfaces;

namespace RetoTecnico.Aplicacion.Transaction.CasoUso;

public class TransactionService(IMapper mapper, IProducerKafkaAdapter producerKafkaAdapter, ITransactionRepository transactionRepository) : ITransactionService
{
    private IMapper Mapper { get; } = mapper;

    public TransactionDto Agregar(TransactionDto entidad)
    {
        var transactionToAdd = Mapper.Map<Dominio.Models.Transaction>(entidad);

        var response = transactionRepository.Agregar(transactionToAdd);

        transactionRepository.GuardarTodosLosCambios();

        return Mapper.Map<TransactionDto>(response);
    }

    public void Editar(TransactionDto entidad)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public async Task<bool> ValidateTransaction(AddTransactionDto addTransactionDto, string server, string topic)
    {
        var valueToValidate = addTransactionDto.Value.ToJson();

        var request = new TransactionBaseRequest
        {
            Message = valueToValidate,
            Server = server,
            Topic = topic,
        };

        var response = await producerKafkaAdapter.ExecuteAsync(request);

        return await Task.FromResult(response);
    }
}
