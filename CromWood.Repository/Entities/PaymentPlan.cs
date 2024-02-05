namespace CromWood.Data.Entities
{
    public class PaymentPlan: DBTable
    {
        public Guid Id { get; set; }
        public Guid ReferenceStatementId { get; set; }
        public TenancyStatement ReferenceStatement { get; set; }
        public Guid? ToPaidBy { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }

        // Installtalment Detail:
        public Guid? InstallmentType { get; set; }
        public int NoOfInstallment { get; set; }
        public float IntrestCharge { get; set; }
        public float InstallmentAmount { get; set; }
        public DateTime InstallmentStart { get; set; }
        public ICollection<PaymentPlanInstallment> Installments { get; set; }
    }
}
