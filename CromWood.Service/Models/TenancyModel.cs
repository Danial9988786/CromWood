using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class TenancyModel
    {
        public Guid Id { get; set; }
        public string TenancyId { get; set; }

        [Required]
        public Guid TenancyTypeId { get; set; }
        public TenantType TenancyType { get; set; }

        [Required]
        public Guid PropertyId { get; set; }
        public PropertyModel Property { get; set; }

        [Required]
        public Guid ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }

        public int NoOfTenants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Contract Rent
        public float RentAmount { get; set; }
        public Guid RentFrequencyId { get; set; }
        public RentFrequency RentFrequency { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public float? SecurityDeposit { get; set; }
        public bool SplitBetweenTenants { get; set; }
        public bool ScheduleRentStatement { get; set; }
        public int? StatementDueDay { get; set; }

        // Bank details
        [MaxLength(100)]
        public string BankName { get; set; }

        [MaxLength(100)]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        public string BankCode { get; set; }

        // Move in charges
        public Guid? TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime? MoveInDate { get; set; }
        public Guid? PaidBy { get; set; } // Tenant Id
        public bool? ContactFeeApplicable { get; set; }
        public float? ContractFee { get; set; }

        [MaxLength(500)]
        public string TransactionDescription { get; set; }
        public List<TenancyTenantModel> TenancyTenants { get; set; }
    }

    public class TenancyTenantModel
    {
        public Guid Id { get; set; }
        public Guid TenancyId { get; set; }
        public Guid TenantId { get; set; }
        public TenantModel Tenant { get; set; }
        public bool IsNew { get; set; }
    }
}
