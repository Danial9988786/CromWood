namespace CromWood.Business.Models
{
    public class StatementTransactionModel
    {
        public Guid Id { get; set; }
        public string PaidBy { get; set; } // It can be housing benefit or Tenant.
        public Guid? PaidByTenantId { get; set; }
        public Guid? StatementId { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid TransactionModeId { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDescription { get; set; }
    }
}
