using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class RecurringCharge: DBTable
    {
        public Guid Id { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
        public string ChargerFor { get; set; }
        public bool Active { get; set; }
        public Guid? RecurringBasisForId { get; set; }
        public Tenant RecurringBasisFor { get; set; }
        public Guid RecurringFrequencyId { get; set; }
        public RecurringFrequency RecurringFrequency { get; set; }
        public DateTime InvoiceRaisedDate { get; set; }
        public DateTime InvoiceRaisedToDate { get; set; }
        public DateTime ExpectedPaymentDueDate { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionDescription { get; set; }
        public float NetAmount { get; set; }
    }
}
