using RetoTecnico.Dominio.Interfaces.Repositorios;

namespace RetoTecnico.Aplicacion.Interfaces.Repository;

public interface ITransactionRepository : IRepositorioBase<Dominio.Models.Transaction, long>;