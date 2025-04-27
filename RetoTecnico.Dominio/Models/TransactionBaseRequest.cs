namespace RetoTecnico.Dominio.Models;

public class TransactionBaseRequest
{
    public string Server { get; set; }
    public string Topic { get; set; }
    public string Message { get; set; }
}