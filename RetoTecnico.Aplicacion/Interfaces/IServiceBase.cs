using RetoTecnico.Dominio.Interfaces;

namespace RetoTecnico.Aplicacion.Interfaces;

public interface IServicioBase<TEntidad, TEntidadId>
    : IAgregar<TEntidad>, IEditar<TEntidad>, IEliminar<TEntidadId>, IListar<TEntidad, TEntidadId>{
}
