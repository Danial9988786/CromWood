namespace CromWood.Business.Models
{
    public class PaymentPlanInstallmentModel
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public float Paid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
