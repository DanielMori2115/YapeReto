using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;

namespace RetoTecnico.Infraestructura.PostgreSql.Repositorios;

public class TransactionRepository(IServiceProvider serviceProvider) : ITransactionRepository
{
    private readonly NpgsqlContext _context = serviceProvider.GetService<NpgsqlContext>();
    private IDbContextTransaction _transactionScope;

    public Transaction Agregar(Transaction entidad)
    {
        _context.Transactions.Add(entidad);
        return entidad;
    }

    public void BeginTransaction()
    {
        var isRelational = _context.Database.IsRelational();

        if(isRelational) _transactionScope = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        var isRelational = _context.Database.IsRelational();

        if (isRelational) _transactionScope?.Commit();
    }

    public void RollbackTransaction()
    {
        var isRelational = _context.Database.IsRelational();

        if (isRelational) _transactionScope?.Rollback();
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
