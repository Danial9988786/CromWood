namespace CromWood.Business.Models
{
    public class PaymentPlanModel
    {
        public Guid Id { get; set; }
        public Guid ReferenceStatementId { get; set; }
        public StatementModel ReferenceStatement { get; set; }
        public Guid? ToPaidBy { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public Guid? InstallmentTypeId { get; set; }
        public int NoOfInstallment { get; set; }
        public float IntrestCharge { get; set; }
        public float InstallmentAmount { get; set; }
        public DateTime InstallmentStart { get; set; }
    }
}
