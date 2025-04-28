using Microsoft.EntityFrameworkCore.Storage;

using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;

namespace RetoTecnico.Infraestructura.PostgreSql.Repositorios;

public class TransactionRepository(NpgsqlContext context) : ITransactionRepository
{
    private readonly NpgsqlContext _context = context;
    private IDbContextTransaction _transactionScope;

    public Transaction Agregar(Transaction entidad)
    {
        context.Transactions.Add(entidad);
        return entidad;
    }

    public void BeginTransaction()
    {
        _transactionScope = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transactionScope?.Commit();
    }

    public void RollbackTransaction()
    {
        _transactionScope?.Rollback();
    }

    public void Editar(Transaction entidad)
    {
        var productoSeleccionado = _context.Transactions.FirstOrDefault(c => c.TransactionId == entidad.TransactionId);

        if (productoSeleccionado == null) return;

        productoSeleccionado.SourceAccountId = entidad.SourceAccountId;
        productoSeleccionado.TargetAccountId = entidad.TargetAccountId;
        productoSeleccionado.Value = entidad.Value;
        productoSeleccionado.TransactionTypeId = entidad.TransactionTypeId;
        productoSeleccionado.TransactionDate = entidad.TransactionDate.ToUniversalTime();

        _context.Entry(productoSeleccionado).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public void Eliminar(long entidadId)
    {
        var productoSeleccionado = _context.Transactions.FirstOrDefault(c => c.TransactionId == entidadId);

        if (productoSeleccionado == null) return;
        
        _context.Transactions.Remove(productoSeleccionado);
    }

    public List<Transaction> Listar()
    {
        return _context.Transactions.ToList();
    }

    public Transaction SeleccionarPorID(long entidadId)
    {
        var productoSeleccionado = _context.Transactions.FirstOrDefault(c => c.TransactionId == entidadId);

        return productoSeleccionado;
    }

    public void GuardarTodosLosCambios()
    {
        _context.SaveChanges();
    }
}
