namespace RetoTecnico.Dominio.Interfaces.Repositorios;

public interface IRepositorioBase<TEntidad, TEntidadID>
    : IAgregar<TEntidad>, IEditar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>, ITransaccion
{
}