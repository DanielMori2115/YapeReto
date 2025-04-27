using AutoMapper;
using RetoTecnico.Aplicacion.AntiFraud.Dto;
using RetoTecnico.Aplicacion.AntiFraud.Interfaces;
using RetoTecnico.Dominio.Interfaces.Repositorios;

namespace RetoTecnico.Aplicacion.AntiFraud.CasoUso
{
    public interface ITransactionService : IServicioBase<TransactionDto, long>;

    public class TransactionService(IMapper mapper, ITransactionRepository transactionRepository) : ITransactionService
    {
        private IMapper Mapper { get; } = mapper;

        public TransactionDto Agregar(TransactionDto entidad)
        {
            throw new NotImplementedException();
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
    }
}
