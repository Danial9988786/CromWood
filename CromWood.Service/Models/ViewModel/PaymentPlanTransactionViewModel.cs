namespace CromWood.Business.Models.ViewModel
{
    public class PaymentPlanTransactionViewModel
    {
        public Guid Id { get; set; }
        public Guid PaymentPlanId { get; set; }
        public string ReferenceID { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
    }
}
