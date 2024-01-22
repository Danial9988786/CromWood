namespace CromWood.Data.Entities
{
    public class PaymentPlanInstallment : DBTable
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public float Paid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
