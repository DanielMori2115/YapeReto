using RetoTecnico.Dominio.Interfaces.Repositorios;

namespace RetoTecnico.Aplicacion.Transaction.Interfaces;

public interface ITransactionRepository : IRepositorioBase<Dominio.Models.Transaction, long>;