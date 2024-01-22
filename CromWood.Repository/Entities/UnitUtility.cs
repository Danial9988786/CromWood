using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class UnitUtility : DBTable
    {
        public Guid Id { get; set; }
        public string MeterSerialNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Note { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
        public Guid UtilityTypeId { get; set; }
        public UtilityType UtilityType { get; set; }
        public Guid UtilityProviderId { get; set; }
        public UtilityProvider UtilityProvider { get; set; }

    }
}
