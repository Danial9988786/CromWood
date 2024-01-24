namespace CromWood.Business.Models
{
    public class UnitUtilityModel
    {
        public Guid Id { get; set; }
        public string MeterSerialNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Note { get; set; }
        public Guid TenancyId { get; set; }
        public Guid UtilityTypeId { get; set; }
        public Guid UtilityProviderId { get; set; }
    }
}
