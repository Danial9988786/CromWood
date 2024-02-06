namespace CromWood.Business.Models
{
    public class PaymentPlanTransactionModel
    {
        public Guid Id { get; set; }
        public string PaidBy { get; set; }
        public Guid? PaidByTenantId { get; set; }
        public Guid PaymentPlanId { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid TransactionModeId { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDescription { get; set; }
    }
}
