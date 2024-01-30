namespace CromWood.Business.Models.ViewModel
{
    public class PaymentPlanInstallmentViewModel
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public float Paid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
