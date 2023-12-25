using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class AssetModel: DBTable
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string AssetId { get; set; }
        public Guid? AssetTypeId { get; set; }
        public AssetType? AssetType { get; set; }

        // Address details
        [MaxLength(100)]
        public string HouseNoStreet { get; set; }
        [MaxLength(100)]
        public string Locality { get; set; }
        [MaxLength(100)]
        public string Borough { get; set; }
        [MaxLength(25)]
        public string PostCode { get; set; }

        // Valuation & ownership
        [MaxLength(100)]
        public string TitleNumber { get; set; }
        [MaxLength(100)]
        public string Ownership { get; set; }
        public DateTime? AquisitionDate { get; set; }
        public float? PurchasePrice { get; set; }
        public float? Valuation { get; set; }
        public DateTime? ValuationDate { get; set; }
        [MaxLength(100)]
        public string Reinstatement { get; set; }

        // Financial details
        [MaxLength(100)]
        public string Lender { get; set; }
        [MaxLength(100)]
        public string Chargee { get; set; }
        public DateTime? DateOfCharge { get; set; }
        public Guid FinancialPrgoramId { get; set; }
        public FinancialPrgoram FinancialPrgoram { get; set; }
        [MaxLength(100)]
        public string GrantProvider { get; set; }
        public float? AttributableGrant { get; set; }
        [MaxLength(100)]
        public string ConstructionPeriod { get; set; }
        public bool LandlordResponsible { get; set; }
        public bool FreeholderResponsible { get; set; }
        public bool OwnerResponsible { get; set; }

        // Landlord Management
        [MaxLength(100)]
        public string LandlordName { get; set; }
        [MaxLength(100)]
        public string ManagingAgent { get; set; }
        [MaxLength(100)]
        public string ManagingAgentHouseNoStreet { get; set; }
        [MaxLength(100)]
        public string ManagingAgentLocality { get; set; }
        [MaxLength(100)]
        public string ManagingAgentBorough { get; set; }
        [MaxLength(25)]
        public string ManagingAgentPostCode { get; set; }
        public DateTime? LeaseTerm { get; set; }
        public DateTime? LeaseExpiry { get; set; }
    }
}
