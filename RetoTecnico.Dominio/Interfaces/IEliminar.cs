namespace RetoTecnico.Dominio.Interfaces;

public interface IEliminar<TEntidadID> {
    void Eliminar(TEntidadID entidadId);
}