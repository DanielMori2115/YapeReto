namespace RetoTecnico.Dominio.Interfaces;

public interface IListar<TEntidad, TEntidadID> {
    List<TEntidad> Listar();

    TEntidad SeleccionarPorID(TEntidadID entidadId);
}