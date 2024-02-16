namespace CromWood.Data.Entities
{
    public class PaymentPlanTransaction : DBTable
    {
        public Guid Id { get; set; }
        public Guid PaymentPlanId { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
        public string ReferenceID { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }

    }
}
