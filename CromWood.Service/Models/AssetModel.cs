using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class AssetModel: DBTable
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Asset ID")]
        public string AssetId { get; set; }
        [Display(Name = "Asset Type")]
        public Guid? AssetTypeId { get; set; }
        public AssetType AssetType { get; set; }

        // Address details
        [MaxLength(100)]
        [Display(Name = "House & Street")]
        public string HouseNoStreet { get; set; }
        [MaxLength(100)]
        [Display(Name = "Locality")]
        public string Locality { get; set; }
        [MaxLength(100)]
        [Display(Name = "Borough")]
        public string Borough { get; set; }
        [MaxLength(25)]
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }

        // Valuation & ownership
        [MaxLength(100)]
        [Display(Name = "Title Number")]
        public string TitleNumber { get; set; }
        [MaxLength(100)]
        [Display(Name = "Ownership")]
        public string Ownership { get; set; }

        [Display(Name = "Aquisition Date")]
        public DateTime? AquisitionDate { get; set; }

        [Display(Name = "Purchase Price")]
        public float? PurchasePrice { get; set; }
        [Display(Name = "Valuation")]
        public float? Valuation { get; set; }
        [Display(Name = "Valuation Date")]
        public DateTime? ValuationDate { get; set; }
        [MaxLength(100)]
        
        public string Reinstatement { get; set; }

        // Financial details
        [MaxLength(100)]
        [Display(Name = "Lender")]
        public string Lender { get; set; }
        [MaxLength(100)]
        [Display(Name = "Chargee")]
        public string Chargee { get; set; }
        [Display(Name = "Date Of Charge")]
        public DateTime? DateOfCharge { get; set; }
        [Display(Name = "Financial Program")]
        public Guid FinancialPrgoramId { get; set; }
        public FinancialPrgoram FinancialPrgoram { get; set; }
        [MaxLength(100)]
        [Display(Name = "Grant Provider")]
        public string GrantProvider { get; set; }
        [Display(Name = "Attributable Grant")]
        public float? AttributableGrant { get; set; }
        [MaxLength(100)]
        [Display(Name = "Construction Period")]
        public string ConstructionPeriod { get; set; }
        [Display(Name = "Landlord Responsible")]
        public bool LandlordResponsible { get; set; }

        [Display(Name = "Freeholder Responsible")]
        public bool FreeholderResponsible { get; set; }
        [Display(Name = "Owner Responsible")]
        public bool OwnerResponsible { get; set; }

        // Landlord Management
        [MaxLength(100)]
        [Display(Name = "Landlord Name")]
        public string LandlordName { get; set; }
        [MaxLength(100)]
        [Display(Name = "Managing Agent")]
        public string ManagingAgent { get; set; }
        [MaxLength(100)]
        [Display(Name = "Managing Agent HouseNoStreet")]
        public string ManagingAgentHouseNoStreet { get; set; }
        [MaxLength(100)]
        [Display(Name = "Managing Agent Locality")]
        public string ManagingAgentLocality { get; set; }
        [MaxLength(100)]
        [Display(Name = "Managing Agent Borough")]
        public string ManagingAgentBorough { get; set; }
        [MaxLength(25)]
        [Display(Name = "Managing Agent Postal Code")]
        public string ManagingAgentPostCode { get; set; }
        [Display(Name = "Lease Term")]
        public DateTime? LeaseTerm { get; set; }
        [Display(Name = "Lease Expiry")]
        public DateTime? LeaseExpiry { get; set; }

        // For Edit purpose in UI
        public string Section { get; set; }
    }
}
