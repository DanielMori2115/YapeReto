namespace RetoTecnico.Aplicacion.Transaction.Dto
{
    public class AddTransactionDto
    {
        public string SourceAccountId { get; set; }
        public string TargetAccountId { get; set; }
        public int TransferTypeId { get; set; }
        public int Value { get; set; }
    }
}
