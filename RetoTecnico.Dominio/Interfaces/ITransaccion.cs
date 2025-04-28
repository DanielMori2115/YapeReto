namespace RetoTecnico.Dominio.Interfaces;

public interface ITransaccion {
    void GuardarTodosLosCambios();
    public void BeginTransaction();
    public void CommitTransaction();
    public void RollbackTransaction();
}