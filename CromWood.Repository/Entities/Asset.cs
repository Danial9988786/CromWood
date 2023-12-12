using System.Net.Sockets;

namespace CromWood.Data.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string AssetId { get; set; }

        // Address details
        public string HouseNoStreet { get; set; }
        public string Locality { get; set; }
        public string Borough { get; set; }
        public string PostCode { get; set; }

        // Valuation & ownership
        public string TitleNumber { get; set; }
        public string Ownership { get; set; }
        public DateTime AquisitionDate { get; set; }
        public float PurchasePrice { get; set; }
        public float Valuation { get; set; }
        public DateTime ValuationDate { get; set; }
        public string Reinstatement { get; set; }

        // Financial details
        public string Lender {  get; set; }
        public string Chargee {  get; set; }
        public DateTime DateOfCharge { get; set; }
        public string Program { get; set; }
        public string GrantProvider { get; set; }
        public float AttributableGrant { get; set; }
        public string ConstructionPeriod { get; set; }
        public bool LandlordResponsible { get; set; }
        public bool FreeholderResponsible { get; set; }
        public bool OwnerResponsible { get; set; }

        // Landlord Management
        public string LandlordName { get; set; }
        public string ManagingAgent { get; set; }
        public string ManagingAgentHouseNoStreet { get; set; }
        public string ManagingAgentLocality { get; set; }
        public string ManagingAgentBorough { get; set; }
        public string ManagingAgentPostCode { get; set; }
        public DateTime LeaseTerm { get; set; }
        public DateTime LeaseExpiry { get; set; }

    }
}
