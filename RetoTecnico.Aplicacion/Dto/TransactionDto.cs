namespace RetoTecnico.Aplicacion.Dto;

public class TransactionDto
{
    public long TransactionId { get; set; }
    public string SourceAccountId { get; set; }
    public string TargetAccountId { get; set; }
    public DateTime TransactionDate { get; set; }
    public int TransactionTypeId { get; set; }
    public decimal Value { get; set; }
}
