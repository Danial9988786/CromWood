namespace CromWood.Data.Entities
{
    public class Tenancy
    {
        public Guid Id { get; set; }
        public string TenancyType { get; set; }
        public string PropertyType { get; set; }
        public Guid PropertyId { get; set; }
        public string ContractType { get; set; }
        public int NoOfTenants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Contract Rent
        public float RentAmount { get; set; }
        public string RentFrequency { get; set; }
        public string PaymentMethod { get; set; }
        public float SecurityDeposit { get; set; }
        public bool SplitBetweenTenants { get; set; }
        public bool ScheduleRentStatement { get; set; }
        public int StatementDueDay { get; set; }

        // Bank details
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }

    }
}
