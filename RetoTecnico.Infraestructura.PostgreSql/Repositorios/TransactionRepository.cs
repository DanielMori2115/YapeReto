using RetoTecnico.Aplicacion.Transaction.Interfaces;
using RetoTecnico.Dominio.Models;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;

namespace RetoTecnico.Infraestructura.PostgreSql.Repositorios;

public class TransactionRepository(TransactionContext context) : ITransactionRepository
{
    private readonly TransactionContext _context = context;

    public Transaction Agregar(Transaction entidad)
    {
        context.Transactions.Add(entidad);
        return entidad;
    }

    public void Editar(Transaction entidad)
    {
        var productoSeleccionado = _context.Transactions.FirstOrDefault(c => c.TransactionId == entidad.TransactionId);

        if (productoSeleccionado == null) return;

        productoSeleccionado.SourceAccountId = entidad.SourceAccountId;
        productoSeleccionado.TargetAccountId = entidad.TargetAccountId;
        productoSeleccionado.TransactionDate = entidad.TransactionDate;
        productoSeleccionado.Value = entidad.Value;

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
