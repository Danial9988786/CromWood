namespace CromWood.Business.Models
{
    public class PayoutModel
    {
        public string PayoutID { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public List<PayoutTenantModel> Payouts { get; set; }
    }

    public class PayoutTenantModel
    {
        public Guid TenancyId { get; set; }
        public string TenantName { get; set; }
        public string TenancyName { get; set; }
        public float RentAmount { get; set; }
        public string RentFrequency { get; set; }
        public float Amount { get; set; }
    }

}
