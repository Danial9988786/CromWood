using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class PaymentPlanViewModel
    {
        public Guid Id { get; set; }
        public Guid ReferenceStatementId { get; set; }
        public StatementViewModel ReferenceStatement { get; set; }
        public Guid? ToPaidBy { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public InstallmentType InstallmentType { get; set; }
        public int NoOfInstallment { get; set; }
        public float IntrestCharge { get; set; }
        public float InstallmentAmount { get; set; }
        public DateTime InstallmentStart { get; set; }
        public ICollection<PaymentPlanInstallmentViewModel> Installments { get; set; }

        public ICollection<PaymentPlanTransactionViewModel> Transactions { get; set; }

    }
}
