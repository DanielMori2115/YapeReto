namespace RetoTecnico.Dominio.Models;

public class OutboxMessage
{
    public long EventId { get; set; }
    public string EventPayload { get; set; }
    public DateTime EventDate { get; set; }
    public bool IsMessageDispatched { get; set; }
}